using System;
using System.ComponentModel.DataAnnotations;

namespace TitanicPassengers.ViewModels
{
	public class AddBodyViewModel
	{
        [Required(ErrorMessage = "Please enter number of body")]
        [Range(0, 1500000, ErrorMessage = "Number must be positive")]
        public required int Number { get; set; }

        [Required(ErrorMessage = "Please enter boat")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be from 3 to 50 characters")]
        public required string Boat { get; set; }

        public required DateOnly Date { get; set; }

        public int? Id { get; set; } = 0;
    }
}

