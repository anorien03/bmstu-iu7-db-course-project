using System;
using Microsoft.EntityFrameworkCore;
using TitanicPassengers.AppDbContext;
using TitanicPassengers.Models;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.Repositories
{
	public class LifeboatRepository
	{
        private IAppDbContextFactory _contextFactory;
        public LifeboatRepository(IAppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public async Task<int> AddAsync(Lifeboat lifeboat, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);

            if (await context.Lifeboats.FindAsync(lifeboat.Id) != null)
                throw new InvalidDataException("Lifeboat with that number already exists");

            await context.Lifeboats.AddAsync(lifeboat);
            await context.SaveChangesAsync();
            return lifeboat.Id;
        }


        public async Task RemoveAsync(int id, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);

            var lifeboat = await context.Lifeboats.FindAsync(id);
            if (lifeboat != null)
            {
                context.Lifeboats.Remove(lifeboat);
                await context.SaveChangesAsync();
            }
        }


        public async Task UpdateAsync(Lifeboat updatedLifeboat, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            var lifeboat = await context.Lifeboats.FindAsync(updatedLifeboat.Id);

            if (lifeboat != null)
            {
                lifeboat.Boat = updatedLifeboat.Boat;
                lifeboat.MaxCount = updatedLifeboat.MaxCount;

                await context.SaveChangesAsync();
            }
        }


        public async Task<Lifeboat> GetByIdAsync(int id, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            return await context.Lifeboats.FindAsync(id) ?? throw new InvalidDataException("Lifeboat not found");

        }


        public async Task<List<Lifeboat>> GetAllAsync(Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            return await context.Lifeboats.ToListAsync();
            
        }
    }
}

