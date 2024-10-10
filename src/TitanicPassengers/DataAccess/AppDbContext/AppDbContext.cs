using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using TitanicPassengers.Models;
using TitanicPassengers.Models.Enums;

namespace TitanicPassengers.AppDbContext
{
	public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Participant> Participants { get; set; } = null!;

        public DbSet<ParticipantStatus> ParticipantStatuses { get; set; } = null!;

        public DbSet<Passenger> Passengers { get; set; } = null!;

        public DbSet<Lifeboat> Lifeboats { get; set; } = null!;

        public DbSet<Body> Bodies { get; set; } = null!;

        public DbSet<CloseRelative> CloseRelatives { get; set; } = null!;


        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        public AppDbContext(DbContextOptions options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<Participant>()
            .HasMany(c => c.CloseRelatives)
            .WithMany(s => s.Participants)
            .UsingEntity(j => j.ToTable("participants_close_relatives"));


            modelBuilder
            .Entity<User>()
            .Property(e => e.Role)
            .HasConversion(
            v => v.ToString(),
            v => (Role)Enum.Parse(typeof(Role), v));


            modelBuilder
            .Entity<ParticipantStatus>()
            .Property(e => e.Status)
            .HasConversion(
            v => v.ToString(),
            v => (Status)Enum.Parse(typeof(Status), v));

            modelBuilder
            .Entity<Participant>()
            .Property(e => e.Gender)
            .HasConversion(
            v => v.ToString(),
            v => (Gender)Enum.Parse(typeof(Gender), v));

            modelBuilder
            .Entity<Passenger>()
            .Property(e => e.PassengerClass)
            .HasConversion(
            v => v.ToString(),
            v => (PassengerClass)Enum.Parse(typeof(PassengerClass), v));

            modelBuilder
            .Entity<CloseRelative>()
            .Property(e => e.Gender)
            .HasConversion(
            v => v.ToString(),
            v => (Gender)Enum.Parse(typeof(Gender), v));
        }
	}
}

