using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class CostingRMComponent
    { 
        public long CostingRMComponentID { get; set; }
        public long CostingID { get; set; }
        //public long CostingRMID { get; set; }  instead Ritemid is used.
        public long RitemId { get; set; }
        
        public long CompID { get; set; } 
        public double Length { get; set; }
        public double Breadth { get; set; }
        public double Height { get; set; }
        public int CompQty { get; set; }
        public double Area { get; set; }
        public int SerialNo { get; set; }

        public int CompGroupId { get; set; }
        public string Remark { get; set; }
        public string Photo { get; set; }
        public double RMQty { get; set; }
        public long SupplierId { get; set; }

        public int ProcessID { get; set; }

        public virtual CostingMaster CostingMaster { get; set; }
    }
}
