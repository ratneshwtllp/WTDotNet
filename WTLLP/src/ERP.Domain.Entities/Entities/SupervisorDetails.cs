using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class SupervisorDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
        public int? ContrOrSuper { get; set; }
    }
}
