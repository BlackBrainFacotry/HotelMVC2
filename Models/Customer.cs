using System.Collections.Generic;

namespace HotelMVC2.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual List<Booking> Bookings { get; set; }

        public int BookingsCount { get; set; }
    }
}
