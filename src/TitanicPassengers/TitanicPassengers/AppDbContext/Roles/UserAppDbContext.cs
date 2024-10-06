using System;
using Microsoft.EntityFrameworkCore;

namespace TitanicPassengers.AppDbContext.Roles
{
	public class UserAppDbContext: AppDbContext
	{
        public UserAppDbContext(DbContextOptions<UserAppDbContext> options)
        : base(options) { }
    }
}

