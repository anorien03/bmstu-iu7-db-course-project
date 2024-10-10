using System;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.AppDbContext
{
	public interface IAppDbContextFactory
	{
        AppDbContext GetDbContext(Role? role);

    }
}

