using System;
using Microsoft.EntityFrameworkCore;
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


        public async Task<int> AddAsync(CloseRelative closeRelative, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            await context.CloseRelatives.AddAsync(closeRelative);
            await context.SaveChangesAsync();
            return closeRelative.Id;
        }


        public async Task RemoveAsync(int id, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);

            var relative = await context.CloseRelatives.FindAsync(id);
            if (relative != null)
            {
                context.CloseRelatives.Remove(relative);
                await context.SaveChangesAsync();
            }
        }


        public async Task UpdateAsync(CloseRelative updatedRelative, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            var relative = await context.CloseRelatives.FindAsync(updatedRelative.Id);

            if (relative != null)
            {
                relative.Name = updatedRelative.Name;
                relative.Surname = updatedRelative.Surname;
                relative.Birthday = updatedRelative.Birthday;
                relative.Gender = updatedRelative.Gender;

                await context.SaveChangesAsync();
            }
        }


        public async Task<CloseRelative> GetByIdAsync(int id, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            return await context.CloseRelatives.FindAsync(id) ?? throw new InvalidDataException("Relative not found");

        }

        public async Task<CloseRelative> GetByNameAsync(string name, string surname, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            return await context.CloseRelatives.FirstOrDefaultAsync(r => r.Name.Equals(name) && r.Surname.Equals(surname)) ?? throw new InvalidDataException("Relative not found");
            
        }


        public async Task<List<CloseRelative>> GetAllAsync(Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            return await context.CloseRelatives.ToListAsync();

        }


    }
}

