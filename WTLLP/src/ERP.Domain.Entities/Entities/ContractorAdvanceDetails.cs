using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ContractorAdvanceDetails
    {
        public long ContractorAdvanceId { get; set; }
        public long ContractorId { get; set; }
        public DateTime ContractorAdvanceDate { get; set; } 
        public double ContractorAdvanceAmount { get; set; } 
        public string ContractorAdvanceRemark { get; set; }
        public string AmountInWords { get; set; }

        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
    }
}
