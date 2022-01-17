using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class AdjustmentRMDetails
    { 
        public long AdjustmentRMStockID { get; set; }
        public long RitemID { get; set; }
        public long SupplierID { get; set; }
        public long RackID { get; set; }
        public double Quantity { get; set; }
        public int StockType { get; set; }
        public string Remark { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public string LotNo { get; set; }

        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserID { get; set; }

    }
}
