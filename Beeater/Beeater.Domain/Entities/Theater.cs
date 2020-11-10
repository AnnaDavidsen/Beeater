using System;
using System.Collections.Generic;

#nullable disable

namespace Beeater.Domain.Entities
{
    public partial class Theater
    {
        public Theater()
        {
            Seats = new HashSet<Seat>();
            Shows = new HashSet<Show>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }
        public virtual ICollection<Show> Shows { get; set; }
    }
}
