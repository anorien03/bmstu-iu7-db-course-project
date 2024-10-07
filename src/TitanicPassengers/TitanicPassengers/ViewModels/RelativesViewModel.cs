using System;
using TitanicPassengers.Models;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.ViewModels
{
	public class RelativesViewModel
	{
        public List<CloseRelative> Relatives { get; set; } = new List<CloseRelative>();

        public Role? Role { get; set; } = null;
    }
}

