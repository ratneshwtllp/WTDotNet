using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class POTransportDetails
    {
        public int POTransportId { get; set; }
        public string POTransport { get; set; } 
        public string POTransportDesc { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
    }
}
