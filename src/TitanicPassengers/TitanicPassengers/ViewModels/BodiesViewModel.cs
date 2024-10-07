using System;
using TitanicPassengers.Models;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.ViewModels
{
	public class BodiesViewModel
	{
        public List<Body> Bodies { get; set; } = new List<Body>();

        public Role? Role { get; set; } = null;
    }
}

