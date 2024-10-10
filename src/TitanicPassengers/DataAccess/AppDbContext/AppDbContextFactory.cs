using System;
using TitanicPassengers.AppDbContext.Roles;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.AppDbContext
{
	public class AppDbContextFactory: IAppDbContextFactory
	{
		private readonly AdminAppDbContext _adminAppDbContext;
        private readonly UserAppDbContext _userAppDbContext;
        private readonly GuestDbContext _guestDbContext;

        public AppDbContextFactory(AdminAppDbContext adminAppDbContext, UserAppDbContext userAppDbContext, GuestDbContext guestDbContext)
		{
			_adminAppDbContext = adminAppDbContext;
			_userAppDbContext = userAppDbContext;
			_guestDbContext = guestDbContext;
		}

		public AppDbContext GetDbContext(Role? role)
		{
			if (role == null)
				return _guestDbContext;
			else if (role == Role.Admin)
				return _adminAppDbContext;
			else
				return _userAppDbContext;					
			
		}
	}
}

