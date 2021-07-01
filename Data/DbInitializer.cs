using HotelMVC2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelMVC2.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HotelBookingContext context)
        {
            // Delete the database, if it already exists. I do this because an
            // existing database may not be compatible with the entity model,
            // if the entity model was changed since the database was created.
            context.Database.EnsureDeleted();

            // Create the database, if it does not already exists. This operation
            // is necessary, if you use an SQL Server database.
            context.Database.EnsureCreated();

            // Look for any bookings.
            if (context.Booking.Any())
            {
                return;   // DB has been seeded
            }

            List<Customer> customers = new List<Customer>
            {
                new Customer { Name="John Smith", Email="js@gmail.com" },
                new Customer { Name="Jane Doe", Email="jd@gmail.com" }
            };

            List<Room> rooms = new List<Room>
            {
                new Room { Description="A" },
                new Room { Description="B" },
                new Room { Description="C" }
            };

            DateTime date = DateTime.Today.AddDays(4);
            List<Booking> bookings = new List<Booking>
            {
                new Booking { StartDate=date, EndDate=date.AddDays(1), IsActive=true, CustomerId=1, RoomId=1 },
                new Booking { StartDate=date.AddDays(1), EndDate=date.AddDays(5), IsActive=true, CustomerId=2, RoomId=2 },
                new Booking { StartDate=date.AddDays(7), EndDate=date.AddDays(14), IsActive=true, CustomerId=1, RoomId=3 },
                new Booking { StartDate=date.AddDays(10), EndDate=date.AddDays(15), IsActive=true, CustomerId=2, RoomId=2 },
                new Booking { StartDate=date.AddDays(17), EndDate=date.AddDays(24), IsActive=true, CustomerId=1, RoomId=3 },
                    new Booking { StartDate=date.AddDays(10), EndDate=date.AddDays(11), IsActive=true, CustomerId=1, RoomId=1 },
                new Booking { StartDate=date.AddDays(30), EndDate=date.AddDays(35), IsActive=true, CustomerId=2, RoomId=2 },
                new Booking { StartDate=date.AddDays(37), EndDate=date.AddDays(44), IsActive=true, CustomerId=1, RoomId=3 },
                new Booking { StartDate=date.AddDays(30), EndDate=date.AddDays(45), IsActive=true, CustomerId=2, RoomId=2 },
                new Booking { StartDate=date.AddDays(37), EndDate=date.AddDays(44), IsActive=true, CustomerId=1, RoomId=3 },
                    new Booking { StartDate=date.AddDays(50), EndDate=date.AddDays(51), IsActive=true, CustomerId=1, RoomId=1 },
                new Booking { StartDate=date.AddDays(51), EndDate=date.AddDays(55), IsActive=true, CustomerId=2, RoomId=2 },
                new Booking { StartDate=date.AddDays(57), EndDate=date.AddDays(64), IsActive=true, CustomerId=1, RoomId=3 },
                new Booking { StartDate=date.AddDays(50), EndDate=date.AddDays(65), IsActive=true, CustomerId=2, RoomId=2 },
                new Booking { StartDate=date.AddDays(67), EndDate=date.AddDays(74), IsActive=true, CustomerId=1, RoomId=3 },
                    new Booking { StartDate=date.AddDays(80), EndDate=date.AddDays(81), IsActive=true, CustomerId=1, RoomId=1 },
                new Booking { StartDate=date.AddDays(71), EndDate=date.AddDays(75), IsActive=true, CustomerId=2, RoomId=2 },
                new Booking { StartDate=date.AddDays(77), EndDate=date.AddDays(84), IsActive=true, CustomerId=1, RoomId=3 },
                new Booking { StartDate=date.AddDays(70), EndDate=date.AddDays(85), IsActive=true, CustomerId=2, RoomId=2 },
                new Booking { StartDate=date.AddDays(77), EndDate=date.AddDays(84), IsActive=true, CustomerId=1, RoomId=3 }

            };

            context.Customer.AddRange(customers);
            context.Room.AddRange(rooms);
            context.SaveChanges();
            context.Booking.AddRange(bookings);
            context.SaveChanges();
        }
    }
}
