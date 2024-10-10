using System;
using TitanicPassengers.AppDbContext;
using TitanicPassengers.Models;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.Repositories
{
	public class PassengerRepository
	{
        private IAppDbContextFactory _contextFactory;
        public PassengerRepository(IAppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public async Task<int> AddAsync(Passenger passenger, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);

            await context.Passengers.AddAsync(passenger);

            await context.SaveChangesAsync();
            return passenger.ParticipantId;
        }



        public async Task UpdateAsync(Passenger updatedPassenger, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            var participant = await context.Passengers.FindAsync(updatedPassenger.ParticipantId);

            if (participant != null)
            {
                participant.PassengerClass = updatedPassenger.PassengerClass;
                participant.Departure = updatedPassenger.Departure;
                participant.Destination = updatedPassenger.Destination;

                await context.SaveChangesAsync();
            }
        }


        public async Task RemoveAsync(int id, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);

            var participant = await context.Passengers.FindAsync(id);
            if (participant != null)
            {
                context.Passengers.Remove(participant);
                await context.SaveChangesAsync();
            }
        }



        public async Task<Passenger> GetByIdAsync(int id, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            return await context.Passengers.FindAsync(id) ?? throw new InvalidDataException();
        }
    }
}

