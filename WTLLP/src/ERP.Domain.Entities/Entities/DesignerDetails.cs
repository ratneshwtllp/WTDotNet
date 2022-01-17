using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class DesignerDetails
    {
        public int DesignerId { get; set; }
        public string DesignerName { get; set; }
        public string DesignerDesc { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserID { get; set; }
    }
}
