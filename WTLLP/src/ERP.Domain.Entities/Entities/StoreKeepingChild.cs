using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class StoreKeepingChild
    { 
        public long StoreKeepingChildID { get; set; }
        public long StoreKeepingID { get; set; }
        public long GRNChildID { get; set; }
        public long RitemID { get; set; } 
        public long RackID { get; set; }
        public double StoreKeepingQty { get; set; }
        public double Side { get; set; }
        public int SNo { get; set; }
        public long SupplierID { get; set; }
        public string LotNo { get; set; }
        public virtual StoreKeepingMaster StoreKeepingMaster { get; set; }
    }
}
