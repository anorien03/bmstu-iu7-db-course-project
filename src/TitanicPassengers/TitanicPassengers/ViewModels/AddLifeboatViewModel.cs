using System;
using System.ComponentModel.DataAnnotations;

namespace TitanicPassengers.ViewModels
{
	public class AddLifeboatViewModel
	{
        [Required(ErrorMessage = "Please enter number of lifeboat")]
        [Range(0, 1500000, ErrorMessage = "Number must be positive")]
        public required int Number { get; set; }

        [Required(ErrorMessage = "Please enter boat")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be from 3 to 50 characters")]
        public required string Boat { get; set; }

        [Required(ErrorMessage = "Please enter max count of lifeboat")]
        [Range(1, 150, ErrorMessage = "Count must be positive")]
        public required int MaxCount { get; set; }

        public int? Id { get; set; } = 0;
    }
}

