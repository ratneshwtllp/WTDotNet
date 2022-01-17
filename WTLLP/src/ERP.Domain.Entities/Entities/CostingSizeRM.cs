using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class CostingSizeRM
    {
        public long CostingSizeRMId { get; set; }
        public long FitemId { get; set; }
        public int SizeId { get; set; }
        public long CostingID { get; set; }
        public long RItemID { get; set; }
        public double RMQty { get; set; }

        public long CompID { get; set; }
        public int CompQty { get; set; }
        public int CompGroupId { get; set; }

        public double CWAS { get; set; }
        public double CAfterWAS { get; set; }
        public double RMPrice { get; set; }
        public double RMCAmount { get; set; }
        public double BOMWAS { get; set; }
        public double BOMAfterWas { get; set; }
        public double RMBAmount { get; set; }
        public int SerialNo { get; set; }
        public int IsComponent { get; set; }
        public long SupplierId { get; set; }
        public string Remark { get; set; }

        public int ProcessID { get; set; }
        

        public virtual CostingMaster CostingMaster { get; set; }

    }
}
