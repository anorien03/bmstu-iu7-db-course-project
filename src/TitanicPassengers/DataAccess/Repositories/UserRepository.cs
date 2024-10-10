using System;
using System.Data;
using Microsoft.EntityFrameworkCore;
using TitanicPassengers.AppDbContext;
using TitanicPassengers.Models;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.Repositories
{
	public class UserRepository
	{
		private IAppDbContextFactory _contextFactory;
		public UserRepository(IAppDbContextFactory contextFactory)
		{
			_contextFactory = contextFactory;
		}


		public async Task<User> Login(string login, string password)
		{
            var user = await GetByLoginAsync(login, Role.User);

            if (user == null)
                throw new InvalidDataException($"User with login {login} not found");
            if (!user.Password.Equals(password))
                throw new InvalidDataException($"Incorrect password");

            return user;
        }


        public async Task<User> Signup(User newUser)
        {
            var context = _contextFactory.GetDbContext(Role.User);
            var user = await GetByLoginAsync(newUser.Login, Role.User);

            if (user != null)
                throw new InvalidDataException($"User with login {newUser.Login} already exists");
            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();

            return newUser;
        }


        public async Task RemoveAsync(int id, Role? role)
		{
            var context = _contextFactory.GetDbContext(role);

            var user = await context.Users.FindAsync(id);
			if (user != null)
			{
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
        }


		public async Task UpdateAsync(User updatedUser, Role? role)
		{
            var context = _contextFactory.GetDbContext(role);
            var user = await context.Users.FindAsync(updatedUser.Id);

            if (user != null)
            {
                user.Login = updatedUser.Login;
                user.Password = updatedUser.Password;
                user.Role = updatedUser.Role;

                await context.SaveChangesAsync();
            }
        }


        public async Task GiveAdminRightsAsync(int id, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            var user = await context.Users.FindAsync(id);

            if (user != null)
            {
                user.Role = Role.Admin;

                await context.SaveChangesAsync();
            }
        }


        public async Task TakeAdminRightsAsync(int id, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            var user = await context.Users.FindAsync(id);

            if (user != null)
            {
                user.Role = Role.User;

                await context.SaveChangesAsync();
            }
        }


        public async Task<List<User>> GetAllAsync(Role? role)
		{
            var context = _contextFactory.GetDbContext(role);
			return await context.Users.ToListAsync();
        }


		public async Task<User?> GetByIdAsync(int id, Role? role)
		{
            var context = _contextFactory.GetDbContext(role);
            return await context.Users.FindAsync(id);
        }


        public async Task<User?> GetByLoginAsync(string login, Role? role)
        {
            var context = _contextFactory.GetDbContext(role);
            return await context.Users.FirstOrDefaultAsync(u => u.Login == login);
        }
    }
}

