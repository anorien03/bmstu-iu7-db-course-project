using System;
using System.ComponentModel.DataAnnotations;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.ViewModels
{
	public class NewRelativeViewModel
	{
        [Required(ErrorMessage = "Please enter name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be from 3 to 50 characters")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Please enter surname")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be from 3 to 50 characters")]
        public required string Surname { get; set; }

        public required Gender Gender { get; set; }

        public required DateOnly Birthday { get; set; }

        public int? Id { get; set; } = 0;
    }
}

