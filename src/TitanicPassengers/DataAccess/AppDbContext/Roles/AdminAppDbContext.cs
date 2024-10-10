using System;
using Microsoft.EntityFrameworkCore;

namespace TitanicPassengers.AppDbContext.Roles
{
	public class AdminAppDbContext: AppDbContext
	{
        public AdminAppDbContext(DbContextOptions<AdminAppDbContext> options)
        : base(options) { }
    }
}

