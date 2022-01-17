using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ProcessChargeHistoryDetails
    {
        public long ProcessChargeHistoryId { get; set; }
        public long FitemId { get; set; } 
        public double PreKnottingCharge { get; set; } 
        public double PreToolingCharge { get; set; } 
        public double NewKnottingCharge { get; set; } 
        public double NewToolingCharge { get; set; } 

        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public int ProductSizeId { get; set; }
    }
}
