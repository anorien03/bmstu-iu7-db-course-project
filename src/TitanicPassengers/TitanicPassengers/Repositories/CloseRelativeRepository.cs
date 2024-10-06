using System;
using TitanicPassengers.AppDbContext;
using TitanicPassengers.Models;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.Repositories
{
	public class CloseRelativeRepository
	{
        private AppDbContextFactory _contextFactory;
        public CloseRelativeRepository(AppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public async Task<int> AddAsync(int participantId, CloseRelative closeRelative, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            var participant = await context.Participants.FindAsync(participantId);
            if (participant == null)
                throw new InvalidDataException("Participant doesn't exist");
            else
            {
                await context.CloseRelatives.AddAsync(closeRelative);
                participant.CloseRelatives.Add(closeRelative);
                await context.SaveChangesAsync();
                return closeRelative.Id;
            }
        }


        public async Task RemoveAsync(int participantId, int closeRelativeId, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            var participant = await context.Participants.FindAsync(participantId);
            if (participant == null)
                throw new InvalidDataException("Participant doesn't exist");

            var closeRelative = await context.CloseRelatives.FindAsync(closeRelativeId);
            if (closeRelative == null)
                throw new InvalidDataException("Close Relative doesn't exist");

            participant.CloseRelatives.Remove(closeRelative);
            await context.SaveChangesAsync();
        }


    }
}

