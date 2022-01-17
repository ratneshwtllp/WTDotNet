using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class IssueRMForChangeChild
    {
        public long IssueRMChangeChildID { get; set; }
        public long IssueRMChangeID { get; set; }
        public long RItem_Id { get; set; }
        public double IssueQty { get; set; }
        public long RackId { get; set; }
        public long SupplierId { get; set; }
        public int Side { get; set; }
        public string Comments { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public int IsAddIn_JWOutBal { get; set; }
        public string HSNCode { get; set; }
        public string TaxFullName { get; set; }
        public string LotNo { get; set; }

        public int TaxId { get; set; }
        public double TaxPer { get; set; }
        public double CGSTPer { get; set; }
        public double CGSTAmt { get; set; }
        public double SGSTPer { get; set; }
        public double SGSTAmt { get; set; }
        public double IGSTPer { get; set; }
        public double IGSTAmt { get; set; }
        public double AfterTax { get; set; }

        public double SQty { get; set; }
        public virtual IssueRMforChangeMaster IssueRMforChangeMaster { get; set; }
    }
}
