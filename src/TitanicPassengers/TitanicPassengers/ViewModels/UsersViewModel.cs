using System;
using TitanicPassengers.Models;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.ViewModels
{
	public class UsersViewModel
	{
        public List<User> Users { get; set; } = new List<User>();

        public Role? Role { get; set; } = null;
    }
}

