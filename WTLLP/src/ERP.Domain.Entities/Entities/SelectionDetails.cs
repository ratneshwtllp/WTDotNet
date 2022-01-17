using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class SelectionDetails
    {
        public int SelectionID { get; set; }
        public string Selection { get; set; }
        public string SelectionDesc { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
