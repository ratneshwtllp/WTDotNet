using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class StitchingDetails
    {
        public int StitchingId { get; set; }
        public string StitchingName { get; set; }
        public string StitchingDesc { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserID { get; set; }
    }
}
