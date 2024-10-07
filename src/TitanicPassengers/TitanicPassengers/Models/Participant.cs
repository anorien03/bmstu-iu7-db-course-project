using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.Models
{
    [Table("participants")]
    public class Participant
	{
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("surname")]
        public string Surname { get; set; }

        [Column("age")]
        public int Age { get; set; }

        [Column("gender")]
        public Gender Gender { get; set; }

        [Column("position")]
        public string? Position { get; set; }

        public ParticipantStatus? ParticipantStatus { get; set; }

        public Passenger? Passenger { get; set; }

        public List<CloseRelative> CloseRelatives { get; set; } = new();


        public Participant(int id, string name, string surname, int age, Gender gender, string? position)
		{
			Id = id;
			Name = name;
			Surname = surname;
			Age = age;
			Gender = gender;
			Position = position;
		}


        public Participant(string name, string surname, int age, Gender gender, string? position)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Gender = gender;
            Position = position;
        }
    }
}

