using System;
using Microsoft.EntityFrameworkCore;

namespace TitanicPassengers.AppDbContext.Roles
{
	public class GuestDbContext: AppDbContext
	{
        public GuestDbContext(DbContextOptions<GuestDbContext> options)
        : base(options) { }
    }
}

