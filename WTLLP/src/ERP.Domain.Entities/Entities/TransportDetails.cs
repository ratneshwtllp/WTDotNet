using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class TransportDetails
    {
        public int TransportId { get; set; }
        public string TranportName { get; set; }
        public string TransportDesc { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
