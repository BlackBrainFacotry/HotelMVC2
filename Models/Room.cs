using System.Collections.Generic;

namespace HotelMVC2.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual List<Booking> Bookings { get; set; }
    }
}
