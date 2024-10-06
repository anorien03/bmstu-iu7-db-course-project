using System;
using TitanicPassengers.AppDbContext;
using TitanicPassengers.Models;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.Repositories
{
	public class ParticipantStatusRepository
	{
        private AppDbContextFactory _contextFactory;
        public ParticipantStatusRepository(AppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public async Task<int> AddAsync(ParticipantStatus participantStatus, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);

            await context.ParticipantStatuses.AddAsync(participantStatus);
            await context.SaveChangesAsync();
            return participantStatus.ParticipantId;
        }



        public async Task UpdateAsync(ParticipantStatus updatedParticipantStatus, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            var participant = await context.ParticipantStatuses.FindAsync(updatedParticipantStatus.ParticipantId);

            if (participant != null)
            {
                participant.Status = updatedParticipantStatus.Status;
                participant.BodyId = updatedParticipantStatus.BodyId;
                participant.LifeboatId = updatedParticipantStatus.LifeboatId;

                await context.SaveChangesAsync();
            }
        }


        public async Task RemoveAsync(int id, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);

            var participant = await context.ParticipantStatuses.FindAsync(id);
            if (participant != null)
            {
                context.ParticipantStatuses.Remove(participant);
                await context.SaveChangesAsync();
            }
        }



        public async Task<ParticipantStatus?> GetByIdAsync(int id, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            return await context.ParticipantStatuses.FindAsync(id);
        }
    }
}

