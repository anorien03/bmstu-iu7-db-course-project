using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.Models
{
    [Table("passengers")]
    public class Passenger
	{
        [Key]
        [Column("participant_id")]
        public int ParticipantId { get; set; }

        [Column("departure")]
        public string Departure { get; set; }

        [Column("destination")]
        public string Destination { get; set; }

        [Column("class")]
        public PassengerClass PassengerClass { get; set; }

        public Participant? Participant { get; set; }


        public Passenger(int participantId, string departure, string destination, PassengerClass passengerClass)
		{
			ParticipantId = participantId;
			Departure = departure;
			Destination = destination;
			PassengerClass = passengerClass;
		}
	}
}

