using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.Models
{
    [Table("close_relatives")]
    public class CloseRelative
	{
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("surname")]
        public string Surname { get; set; }

        [Column("birthday")]
        public DateOnly Birthday { get; set; }

        [Column("gender")]
        public Gender Gender { get; set; }

        public List<Participant>? Participants { get; set; } = new();


        public CloseRelative(int id, string name, string surname, DateOnly birthday, Gender gender)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Birthday = birthday;
            Gender = gender;
        }

        public CloseRelative(string name, string surname, DateOnly birthday, Gender gender)
        {
            Name = name;
            Surname = surname;
            Birthday = birthday;
            Gender = gender;
        }
    }
}

