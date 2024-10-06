using System;
using System.ComponentModel.DataAnnotations;

namespace TitanicPassengers.ViewModels
{
	public class AccountViewModel
	{
        [Required(ErrorMessage = "Please enter login")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be from 3 to 50 characters")]
        public required string Login { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be from 3 to 50 characters")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}

