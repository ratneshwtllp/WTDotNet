using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ProcessChargeDetails
    {
        public long ProcessChargeId { get; set; }
        public long FitemId { get; set; } 
        public double KnottingCharge { get; set; } 
        public double ToolingCharge { get; set; } 

        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public int ProductSizeId { get; set; }
    }
}
