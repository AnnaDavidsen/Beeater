using System;
using System.Collections.Generic;

#nullable disable

namespace Beeater.Domain.Entities
{
    public partial class Show
    {
        public Show()
        {
            Bookings = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public int? MovieId { get; set; }
        public int? TheaterId { get; set; }
        public DateTime ShowTime { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Theater Theater { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
