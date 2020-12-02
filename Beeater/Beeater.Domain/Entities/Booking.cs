using System;
using System.Collections.Generic;

#nullable disable

namespace Beeater.Domain.Entities
{
    public partial class Booking
    {
        public int Id { get; set; }
        public int? ShowId { get; set; }
        public int? SeatId { get; set; }
        public string UserId { get; set; }

        public virtual Seat Seat { get; set; }
        public virtual Show Show { get; set; }
        public virtual User User { get; set; }
    }
}
