using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class WorkPlanChild
    { 
        public long PlanChildId { get; set; }
        public long PlanId { get; set; }
        public long OrderChildId { get; set; }
        public long FitemId { get; set; } 
        public int PlanQty { get; set; } 
        public double LabourCharges { get; set; }
        public string Instruction1 { get; set; }
        public string Instruction2 { get; set; }
        public int ProductSizeId { get; set; }
        public int SNo { get; set; }

        public virtual WorkPlanMaster WorkPlanMaster { get; set; }
    }
}
