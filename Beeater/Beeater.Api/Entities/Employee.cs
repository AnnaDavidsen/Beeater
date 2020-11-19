using System;
using System.Collections.Generic;

#nullable disable

namespace Beeater.Api.Entities
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int? AuthorizationId { get; set; }
        public string Title { get; set; }

        public virtual Authorization Authorization { get; set; }
    }
}
