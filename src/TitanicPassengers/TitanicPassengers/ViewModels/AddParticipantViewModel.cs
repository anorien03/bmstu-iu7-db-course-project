using System;
using System.ComponentModel.DataAnnotations;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.ViewModels
{
	public class AddParticipantViewModel
	{
        [Required(ErrorMessage = "Please enter name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be from 3 to 50 characters")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Please enter surname")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be from 3 to 50 characters")]
        public required string Surname { get; set; }

        public required Gender Gender { get; set; }

        [Range(0, 150, ErrorMessage = "Age must be in range from 0 to 150")]
        public required int Age { get; set; }

        public required bool IsPassenger { get; set; }

        public string? Position { get; set; } = null;
    }
}

