using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class POPaymentTerms
    {
        public int PTId { get; set; }
        public string PT { get; set; } 
        public string PTDesc { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
    }
}
