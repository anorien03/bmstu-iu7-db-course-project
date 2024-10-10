using System;
using TitanicPassengers.AppDbContext;
using TitanicPassengers.Models.Enums;

namespace Experiments
{
	public class TestDbContextFactory: IAppDbContextFactory
	{
        private readonly AppDbContext _context;
		public TestDbContextFactory(AppDbContext context)
		{
            _context = context;
		}

        public AppDbContext GetDbContext(Role? role)
        {
            return _context;
        }
    }
}

