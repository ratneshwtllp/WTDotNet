using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class CostingEl
    { 
        public long CostingElID { get; set; }
        public long CostingID { get; set; }
        public long CostingElementID { get; set; }
        public double Percent { get; set; }
        public double ElementAmount { get; set; } 
        public int SerialNo { get; set; }

        public virtual CostingMaster CostingMaster { get; set; }
    }
}
