﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Beeater.Domain.Entities
{
    public partial class Rating
    {
        public int Id { get; set; }
        public int Rating1 { get; set; }
        public int MovieId { get; set; }
        public string UserId { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual User User { get; set; }
    }
}
