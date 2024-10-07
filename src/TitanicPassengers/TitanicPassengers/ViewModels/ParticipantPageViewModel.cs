using System;
using TitanicPassengers.Models;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.ViewModels
{
	public class ParticipantPageViewModel
	{
		public required Participant Participant { get; set; }

		public ParticipantStatus? Status { get; set; } = null;

		public Passenger? Passenger { get; set; } = null;

		public List<CloseRelative> Relatives { get; set; } = new List<CloseRelative>();

		public Lifeboat? Lifeboat { get; set; } = null;

        public Body? Body { get; set; } = null;

		public Role Role { get; set; }
    }
}

