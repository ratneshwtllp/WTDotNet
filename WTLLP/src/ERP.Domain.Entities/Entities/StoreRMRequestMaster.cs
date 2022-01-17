using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class StockTransferRequest 
    { 
        public long StockTransferRequestId { get; set; } 
        public long StockTransferRequestSNo { get; set; } 
        public string StockTransferRequestNo { get; set; } 
        public DateTime StockTransferRequestDate { get; set; } 
        public long RequestBranchId { get; set; } 
        public long RequestToBranchId { get; set; }
        public string RequestRemark { get; set; }
        public int UserId { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
       
        public virtual ICollection<StockTransferRequestChild> StockTransferRequestChild { get; set; } 
    }
}
