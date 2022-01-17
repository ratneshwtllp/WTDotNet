using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ContractorPayment
    {
        public long ContractorPaymentId { get; set; }
        public long ContractorId { get; set; }
        public int ProcessId { get; set; }
        public double PaidAmount { get; set; } 
        public string PaidTo { get; set; }
        public DateTime PaidDate { get; set; }
        public string Remark { get; set; } 

        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }

        public double PreviousBalance { get; set; }
        public double TotalProcessAmount { get; set; }
        public double TotalAmount { get; set; }
        public double Balance { get; set; }
        public string AmountInWords { get; set; }

        public int CPSNo { get; set; }
        public string CPNo { get; set; }

        public virtual ICollection<ContractorPaymentChild> ContractorPaymentChild { get; set; } 
    }
}
