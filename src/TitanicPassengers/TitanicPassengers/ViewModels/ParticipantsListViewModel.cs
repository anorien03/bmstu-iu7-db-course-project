using System;
using TitanicPassengers.Models;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.ViewModels
{
	public class ParticipantsListViewModel
	{
		public List<Participant> Participants { get; set; } = new List<Participant>();

		public Role? Role { get; set; } = null;
	}
}

