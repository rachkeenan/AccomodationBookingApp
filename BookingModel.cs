using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace BookingApplication
{
    public class BookingAppContext : DbContext
    {
        public DbSet<Accomodation> Accomodations { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public string DbPath { get; }

        public BookingAppContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "bookingApp.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasIndex(c => c.Id)
                .IsUnique();
            
            modelBuilder.Entity<Accomodation>()
                .HasIndex(c => c.Id)
                .IsUnique();
        }
    }

    public class Accomodation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string AccomodationType { get; set; }
        [Required]
        public int NoOfRooms { get; set; }

        public List<Booking> Bookings { get; } = new();

        public override string ToString()
        {
            return $"{Name}\nType: {AccomodationType}\nNumber of Rooms: {NoOfRooms}";
        }
    }

    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int AccomodationId { get; set; }

        public Accomodation Accomodations { get; set; }
    }
}
