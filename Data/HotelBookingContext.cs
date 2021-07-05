using HotelMVC2.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace HotelMVC2.Data
{
    public class HotelBookingContext : DbContext
    {
        public HotelBookingContext(DbContextOptions<HotelBookingContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }


        public DbSet<Booking> Booking { get; set; }

        public DbSet<Room> Room { get; set; }

        public DbSet<Customer> Customer { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().Property(f => f.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Customer>().Property(f => f.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Booking>().Property(f => f.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Description = "A" },
                new Room { Id = 2, Description = "B" },
                new Room { Id = 3, Description = "C" }

                );

            DateTime date = DateTime.Today.AddDays(4);
            modelBuilder.Entity<Booking>().HasData(

                new Booking { Id = 1, StartDate = date, EndDate = date.AddDays(1), IsActive = true, CustomerId = 1, RoomId = 1 },
                new Booking { Id = 2, StartDate = date.AddDays(1), EndDate = date.AddDays(5), IsActive = true, CustomerId = 2, RoomId = 2 },
                new Booking { Id = 3, StartDate = date.AddDays(7), EndDate = date.AddDays(14), IsActive = true, CustomerId = 1, RoomId = 3 },
                new Booking { Id = 4, StartDate = date.AddDays(10), EndDate = date.AddDays(15), IsActive = true, CustomerId = 2, RoomId = 2 },
                new Booking { Id = 5, StartDate = date.AddDays(17), EndDate = date.AddDays(24), IsActive = true, CustomerId = 1, RoomId = 3 },
                new Booking { Id = 6, StartDate = date.AddDays(10), EndDate = date.AddDays(11), IsActive = true, CustomerId = 1, RoomId = 1 },
                new Booking { Id = 7, StartDate = date.AddDays(30), EndDate = date.AddDays(35), IsActive = true, CustomerId = 2, RoomId = 2 },
                new Booking { Id = 8, StartDate = date.AddDays(37), EndDate = date.AddDays(44), IsActive = true, CustomerId = 1, RoomId = 3 },
                new Booking { Id = 9, StartDate = date.AddDays(30), EndDate = date.AddDays(45), IsActive = true, CustomerId = 2, RoomId = 2 },
                new Booking { Id = 10, StartDate = date.AddDays(37), EndDate = date.AddDays(44), IsActive = true, CustomerId = 1, RoomId = 3 }


                );


            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "John Smith", Email = "js@gmail.com" },
                new Customer { Id = 2, Name = "Jane Doe", Email = "jd@gmail.com" }

                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
