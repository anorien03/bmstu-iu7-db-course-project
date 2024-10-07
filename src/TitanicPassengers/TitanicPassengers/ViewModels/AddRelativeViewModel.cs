using System;
using System.ComponentModel.DataAnnotations;

namespace TitanicPassengers.ViewModels
{
	public class AddRelativeViewModel
	{
        [Required(ErrorMessage = "Please enter name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be from 3 to 50 characters")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Please enter surname")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be from 3 to 50 characters")]
        public required string Surname { get; set; }

        public int? Id { get; set; } = 0;
    }
}

