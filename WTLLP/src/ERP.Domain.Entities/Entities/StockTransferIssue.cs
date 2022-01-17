using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class StockTransferIssue 
    { 
        public long StockTransferIssueId { get; set; }
        public string StockTransferIssueNo { get; set; }
        public string GPNo { get; set; }
        public string VehicalNo { get; set; }
        public long StockTransferRequestId { get; set; }
        public int TransportId { get; set; }
        public DateTime StockTransferIssueDate { get; set; }
        public DateTime SupplyDate { get; set; }
        public DateTime GPDate { get; set; }
        public int UserId { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }

        public double TotalAmount { get; set; }
        public double TotalIGST { get; set; }
        public double TotalCGST { get; set; }
        public double TotalSGST { get; set; }
        public double TotalAmountAfterTax { get; set; }
        public string  AmountInWords { get; set; }


        public virtual ICollection<StockTransferIssueChild> StockTransferIssueChild { get; set; }
    }
} 
