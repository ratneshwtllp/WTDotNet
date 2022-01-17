using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ContractorPaymentChild
    {
        public long ContractorPaymentChildId { get; set; }
        public long ContractorPaymentId { get; set; }
        public long PRMasterId { get; set; } 
        public int SNo { get; set; }

        public virtual ContractorPayment ContractorPayment { get; set; }
    }
}
