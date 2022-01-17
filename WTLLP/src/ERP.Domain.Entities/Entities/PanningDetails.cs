using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class PanningDetails
    {
        public int PanningId { get; set; }
        public string PanningName { get; set; }
        public string PanningDesc { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
