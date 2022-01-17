using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class StockTransferRequestChild
    {
        public long StockTransferRequestChildId { get; set; }
        public long StockTransferRequestId { get; set; }
        public long RitemId { get; set; }
        public string RitemRemark { get; set; }
        public double RequestQty { get; set; }
        public string Unit { get; set; }
       

        public virtual StockTransferRequest StockTransferRequest { get; set; } 
    }
}
