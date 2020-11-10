using System;
using System.Collections.Generic;

#nullable disable

namespace Beeater.Domain.Entities
{
    public partial class User
    {
        public User()
        {
            Bookings = new HashSet<Booking>();
            Preferences = new HashSet<Preference>();
            Ratings = new HashSet<Rating>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public int? BonusPoints { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Preference> Preferences { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
