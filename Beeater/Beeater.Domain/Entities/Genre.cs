using System;
using System.Collections.Generic;

#nullable disable

namespace Beeater.Domain.Entities
{
    public partial class Genre
    {
        public Genre()
        {
            Preferences = new HashSet<Preference>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Preference> Preferences { get; set; }
    }
}
