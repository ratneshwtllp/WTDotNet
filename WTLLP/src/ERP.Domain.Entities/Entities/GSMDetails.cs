using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class GSMDetails
    {
        public int GSMId { get; set; }
        public int GSMName { get; set; }
        public string GSMDesc { get; set; } 
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
