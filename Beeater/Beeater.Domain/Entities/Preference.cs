using System;
using System.Collections.Generic;

#nullable disable

namespace Beeater.Api.Beeater.Domain.Entities
{
    public partial class Preference
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual User User { get; set; }
    }
}
