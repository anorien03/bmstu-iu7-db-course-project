using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.Models
{
    [Table("participants_status")]
    public class ParticipantStatus
	{
        [Key]
        [Column("participant_id")]
        public int ParticipantId { get; set; }

        [Column("status")]
        public Status Status { get; set; }

        [Column("lifeboat_id")]
        public int? LifeboatId { get; set; }

        [Column("body_id")]
        public int? BodyId { get; set; }

        public Participant? Participant { get; set; }

        public Lifeboat? Lifeboat { get; set; }

        public Body? Body { get; set; }


        public ParticipantStatus(int participantId, Status status, int? lifeboatId, int? bodyId)
		{
            ParticipantId = participantId;
            Status = status;
            LifeboatId = lifeboatId;
            BodyId = bodyId;
		}
	}
}

