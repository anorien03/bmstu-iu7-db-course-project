using System;
using System.Reflection.Emit;
using TitanicPassengers.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TitanicPassengers.Models
{
    [Table("users")]
    public class User
	{
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("login")]
        public string Login { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("role")]
        public Role Role { get; set; }


        public User(int id, string login, string password, Role role)
        {
            Id = id;
            Login = login;
            Password = password;
            Role = role;
        }


        public User(string login, string password, Role role)
        {
            Login = login;
            Password = password;
            Role = role;
        }
    }
}

