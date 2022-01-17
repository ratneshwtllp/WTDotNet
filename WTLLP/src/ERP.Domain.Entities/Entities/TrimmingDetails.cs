using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class TrimmingDetails
    {
        public int TrimmingId { get; set; }
        public string TrimmingName { get; set; }
        public string TrimmingDesc { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserID { get; set; }
    }
}
