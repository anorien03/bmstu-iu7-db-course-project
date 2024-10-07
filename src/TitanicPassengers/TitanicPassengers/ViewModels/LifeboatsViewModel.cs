using System;
using TitanicPassengers.Models;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.ViewModels
{
    public class LifeboatsViewModel
    {
        public List<Lifeboat> Lifeboats { get; set; } = new List<Lifeboat>();

        public Role? Role { get; set; } = null;
    }
}

