using System;
using System.Collections.Generic;

#nullable disable

namespace Beeater.Domain.Entities
{
    public partial class Trailer
    {
        public int Id { get; set; }
        public int? Movieid { get; set; }
        public string Path { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
