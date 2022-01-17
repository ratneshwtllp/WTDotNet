using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class PODeliveryTerms
    {
        public int DTId { get; set; }
        public string DT { get; set; } 
        public string DTDesc { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
    }
}
