using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.Models
{
    [Table("lifeboats")]
    public class Lifeboat
	{
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("boat")]
        public string Boat { get; set; }

        [Column("max_count")]
        public int MaxCount { get; set; }

        [Column("survived_count")]
        public int SurvivedCount { get; set; }

        public List<ParticipantStatus>? ParticipantStatuses { get; set; }


        public Lifeboat(int id, string boat, int maxCount, int survivedCount)
		{
            Id = id;
            Boat = boat;
            MaxCount = maxCount;
            SurvivedCount = survivedCount;
		}

        public Lifeboat(string boat, int maxCount, int survivedCount)
        {
            Boat = boat;
            MaxCount = maxCount;
            SurvivedCount = survivedCount;
        }
    }
}

