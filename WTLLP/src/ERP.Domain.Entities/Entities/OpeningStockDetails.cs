using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class OpeningStockDetails
    { 
        public long OpeningStockID { get; set; }
        public long RitemID { get; set; }
        public long SupplierID { get; set; }
        public long RackID { get; set; }
        public double OpeningStockQty { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserID { get; set; }
        public double Side { get; set; }
        public string LotNo { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
    }
}
