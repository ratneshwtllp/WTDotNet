using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class RMBrandDetails
    { 
        public int RMBrandId { get; set; }
        public string RMBrandName { get; set; }
        public string RMBrandCode { get; set; }
        public string RMBrandDesc { get; set; } 
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
        
    }
}
