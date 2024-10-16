﻿using System;
using Microsoft.EntityFrameworkCore;
using TitanicPassengers.AppDbContext;
using TitanicPassengers.Models;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.Repositories
{
	public class ParticipantRepository
	{
        private IAppDbContextFactory _contextFactory;
        public ParticipantRepository(IAppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public async Task<int> AddAsync(Participant participant, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);

            await context.Participants.AddAsync(participant);
            await context.SaveChangesAsync();
            return participant.Id;
        }


        public async Task RemoveAsync(int id, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);

            var participant = await context.Participants.FindAsync(id);
            if (participant != null)
            {
                context.Participants.Remove(participant);
                await context.SaveChangesAsync();
            }
        }


        public async Task UpdateAsync(Participant updatedParticipant, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            var participant = await context.Participants.FindAsync(updatedParticipant.Id);

            if (participant != null)
            {
                participant.Name = updatedParticipant.Name;
                participant.Surname = updatedParticipant.Surname;
                participant.Age = updatedParticipant.Age;
                participant.Gender = updatedParticipant.Gender;
                participant.Position = updatedParticipant.Position;

                await context.SaveChangesAsync();
            }
        }


        public async Task<List<Participant>> GetAllAsync(Role? role, Status? status = null)
        {
            var context = _contextFactory.GetDbContext(role);

            if (role == null)
                return await context.Participants.ToListAsync();
            else
            {
                if (status == null)
                    return await context.Participants.Include(p => p.ParticipantStatus).ToListAsync();
                else
                {
                    return await context.Participants.Include(p => p.ParticipantStatus).Where(p => p.ParticipantStatus != null && p.ParticipantStatus.Status == status).ToListAsync();
                }
            }
                
        }


        public async Task<Participant> GetByIdAsync(int id, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            return await context.Participants.Include(p => p.ParticipantStatus).Include(p => p.Passenger)
                .Include(p => p.CloseRelatives).FirstOrDefaultAsync(p => p.Id == id) ?? throw new InvalidDataException();

        }


        public async Task AddRelativeAsync(int id, CloseRelative relative, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);

            var participant = await context.Participants.Include(p => p.CloseRelatives).FirstOrDefaultAsync(p => p.Id == id);
            
            if (participant != null && participant.CloseRelatives.FirstOrDefault(c => c.Id == relative.Id) == null)
            {
                participant.CloseRelatives.Add(relative);
                await context.SaveChangesAsync();
            }
            else
                throw new InvalidDataException("Participant already has that relative");
        }


        public async Task RemoveRelativeAsync(int participantId, int closeRelativeId, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            var participant = await context.Participants.Include(p => p.CloseRelatives).FirstOrDefaultAsync(p => p.Id == participantId);
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

