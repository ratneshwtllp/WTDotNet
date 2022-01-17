using System;

namespace ERP.Domain.Entities
{
    public class ViewGetRack
    { 
        public long RackId { get; set; }
        public string RackNo { get; set; }
        public long RitemID { get; set; }
        public long SupplierID { get; set; }
        public string Remark { get; set; }
        public long OpeningStockID { get; set; }
        public double OpeningStockQty { get; set; } 
    }
    
}
