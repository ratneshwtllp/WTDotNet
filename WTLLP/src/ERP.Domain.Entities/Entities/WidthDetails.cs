using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class WidthDetails
    {
        public int WidthId { get; set; }
        public string WidthName { get; set; } 
        public string WidthDesc { get; set; } 
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
