using System;
using Microsoft.EntityFrameworkCore;
using TitanicPassengers.AppDbContext;
using TitanicPassengers.Models;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.Repositories
{
	public class BodyRepository
	{
        private IAppDbContextFactory _contextFactory;
        public BodyRepository(IAppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public async Task<int> AddAsync(Body body, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);

            if (await context.Bodies.FindAsync(body.Id) != null)
                throw new InvalidDataException("Body with that number already exists");

            await context.Bodies.AddAsync(body);
            await context.SaveChangesAsync();
            return body.Id;
        }


        public async Task RemoveAsync(int id, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);

            var body = await context.Bodies.FindAsync(id);
            if (body != null)
            {
                context.Bodies.Remove(body);
                await context.SaveChangesAsync();
            }
        }


        public async Task UpdateAsync(Body updatedBody, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            var body = await context.Bodies.FindAsync(updatedBody.Id);

            if (body != null)
            {
                body.Boat = updatedBody.Boat;
                body.Date = updatedBody.Date;

                await context.SaveChangesAsync();
            }
        }


        public async Task<Body> GetByIdAsync(int id, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            return await context.Bodies.FindAsync(id) ?? throw new InvalidDataException("Body not found");

        }


        public async Task<List<Body>> GetAllAsync(Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            return await context.Bodies.ToListAsync();

        }
    }
}

