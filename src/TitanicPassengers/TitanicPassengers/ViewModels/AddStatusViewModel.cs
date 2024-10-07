using System;
using System.ComponentModel.DataAnnotations;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.ViewModels
{
	public class AddStatusViewModel
	{
        public required Status Status { get; set; }

        public int? LifeboatId { get; set; } = null;

        public int? BodyId { get; set; } = null;

        public int? Id { get; set; } = 0;
    }
}

