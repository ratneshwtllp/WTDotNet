using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ProcessExecutionChild
    {
        public long PRChildId { get; set; }
        public long PRMasterId { get; set; }
        public long PlanId { get; set; }
        public long PlanChildId { get; set; }
        public long FitemId { get; set; }
        public string Code { get; set; }
        public int IsComponent { get; set; }
        public int Comp_Id { get; set; }
        public int Comp_Qty { get; set; }
        public int SizeId { get; set; }
        public string SizeName { get; set; }
        public double Charges { get; set; }
        public double Amount { get; set; }
        public string Remark { get; set; }
        public int SNo { get; set; }
        public virtual ProcessExecutionMaster ProcessExecutionMaster { get; set; }
    }
}
