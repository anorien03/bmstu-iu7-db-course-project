using System;
using System.ComponentModel.DataAnnotations;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.ViewModels
{
	public class AddPassengerViewModel
	{
        [Required(ErrorMessage = "Please enter departure")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be from 3 to 50 characters")]
        public required string Departure { get; set; }

        [Required(ErrorMessage = "Please enter destination")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be from 3 to 50 characters")]
        public required string Destination { get; set; }

        public required PassengerClass PassengerClass { get; set; }

        public int? Id { get; set; } = 0;
    }
}

