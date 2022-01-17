using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class BackingDetails
    {
        public int BackingId { get; set; }
        public string BackingName { get; set; }
        public string BackingDesc { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserID { get; set; }
    }
}
