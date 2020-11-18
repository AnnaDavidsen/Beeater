using System;
using System.Collections.Generic;

#nullable disable

namespace Beeater.Persistence.Entities
{
    public partial class Rating
    {
        public int Id { get; set; }
        public int Rating1 { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual User User { get; set; }
    }
}
