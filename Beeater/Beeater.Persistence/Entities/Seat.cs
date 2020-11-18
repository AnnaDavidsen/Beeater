using System;
using System.Collections.Generic;

#nullable disable

namespace Beeater.Persistence.Entities
{
    public partial class Seat
    {
        public Seat()
        {
            Bookings = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }
        public int? TheaterId { get; set; }

        public virtual Theater Theater { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
