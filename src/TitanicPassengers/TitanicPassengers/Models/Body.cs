using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.Models
{
    [Table("bodies")]
    public class Body
	{
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("boat")]
        public string Boat { get; set; }

        [Column("date")]
        public DateOnly Date { get; set; }

        public ParticipantStatus? ParticipantStatus { get; set; }


        public Body(int id, string boat, DateOnly date)
		{
			Id = id;
			Boat = boat;
			Date = date;
		}

        public Body(string boat, DateOnly date)
        {
            Boat = boat;
            Date = date;
        }
    }
}

