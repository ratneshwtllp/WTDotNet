using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class StockTransferIssueChild
    {
        public long StockTransferIssueChildId { get; set; }
        public long StockTransferIssueId { get; set; }
        public long StockTransferRequestChildId { get; set; }
        public long RitemId { get; set; }
        public double IssueQty { get; set; }
        public string Unit { get; set; }
        public int SNo { get; set; }

        public string RMRemark { get; set; }
        public long SupplierId { get; set; }
        public long RackId { get; set; }
        public string LotNo { get; set; }

        public double Rate { get; set; }
        public double Amount { get; set; }
        public string HSNCode { get; set; }
        public string TaxFullName { get; set; }

        public double CGSTPer { get; set; }
        public double CGSTAmt { get; set; }
        public double SGSTPer { get; set; }
        public double SGSTAmt { get; set; }
        public double IGSTPer { get; set; }
        public double IGSTAmt { get; set; }
        public double AfterTax { get; set; }
        public int TaxId { get; set; }
        public double TaxPer { get; set; } 


        public virtual StockTransferIssue StockTransferIssue { get; set; }
    }
}
