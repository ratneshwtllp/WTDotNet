using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class WaxDetails
    {
        public int WaxID { get; set; }
        public string WaxName { get; set; }
        public string WaxDesc { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserID { get; set; }
    }
}
