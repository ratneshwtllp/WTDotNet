using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{ 
    public class ViewRMLotRate
    {
        public long RitemID { get; set; }
        public long SupplierID { get; set; }
        public string LotNo { get; set; } 
        public double Rate { get; set; }
        public long StoreKeepingChildID { get; set; }
    } 

} 
