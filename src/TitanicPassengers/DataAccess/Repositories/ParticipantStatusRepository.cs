using System;
using Microsoft.EntityFrameworkCore;
using TitanicPassengers.AppDbContext;
using TitanicPassengers.Models;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.Repositories
{
	public class ParticipantStatusRepository
	{
        private IAppDbContextFactory _contextFactory;
        public ParticipantStatusRepository(IAppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public async Task<int> AddAsync(ParticipantStatus participantStatus, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);

            //if (participantStatus.Status == Status.Survived && participantStatus.BodyId != null)
            //    throw new InvalidDataException("Survived person can't have body number");
            //if (participantStatus.Status == Status.Victim && participantStatus.LifeboatId != null)
            //    throw new InvalidDataException("Victim can't have lifeboat number");

            //if (participantStatus.LifeboatId != null && await context.Lifeboats.FindAsync(participantStatus.LifeboatId) == null)
            //    throw new InvalidDataException("Lifeboat doesn't exist");

            //if (participantStatus.BodyId != null && await context.Bodies.FindAsync(participantStatus.BodyId) == null)
            //    throw new InvalidDataException("Body doesn't exist");


            await context.ParticipantStatuses.AddAsync(participantStatus);
            await context.SaveChangesAsync();
            return participantStatus.ParticipantId;
        }



        public async Task UpdateAsync(ParticipantStatus updatedParticipantStatus, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            var participant = await context.ParticipantStatuses.FindAsync(updatedParticipantStatus.ParticipantId);

            if (updatedParticipantStatus.Status == Status.Survived && updatedParticipantStatus.BodyId != null)
                throw new InvalidDataException("Survived person can't have body number");
            if (updatedParticipantStatus.Status == Status.Victim && updatedParticipantStatus.LifeboatId != null)
                throw new InvalidDataException("Victim can't have lifeboat number");

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



        public async Task<ParticipantStatus> GetByIdAsync(int id, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            return await context.ParticipantStatuses.FindAsync(id) ?? throw new InvalidDataException();
        }


        public async Task<List<ParticipantStatus>> GetAllAsync(Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            return await context.ParticipantStatuses.Include(p => p.Lifeboat).Include(p => p.Body).ToListAsync();
        }


        public async Task<List<ParticipantStatus>> GetByLifeboatAsync(int lifeboatId, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            return await context.ParticipantStatuses.Include(p => p.Lifeboat).Include(p => p.Body).Where(p => p.LifeboatId == lifeboatId).ToListAsync();
        }
    }
}

