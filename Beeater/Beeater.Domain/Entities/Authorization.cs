using System;
using System.Collections.Generic;

#nullable disable

namespace Beeater.Api.Beeater.Domain.Entities
{
    public partial class Authorization
    {
        public Authorization()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public byte[] Userauth { get; set; }
        public byte[] Movieauth { get; set; }
        public byte[] Showingauth { get; set; }
        public byte[] Sceneauth { get; set; }
        public byte[] Bookingauth { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
