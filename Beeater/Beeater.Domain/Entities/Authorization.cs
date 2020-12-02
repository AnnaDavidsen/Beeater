using System;
using System.Collections.Generic;

#nullable disable

namespace Beeater.Domain.Entities
{
    public partial class Authorization
    {
        public Authorization()
        {
            Employees = new HashSet<Employee>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public bool? Userauth { get; set; }
        public bool? Movieauth { get; set; }
        public bool? Showingauth { get; set; }
        public bool? Sceneauth { get; set; }
        public bool? Bookingauth { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
