using System;

namespace ERP.Domain.Entities
{
    public class ViewWorkPlanBOMRuning
    {  
        public long PlanId { get; set; }
        public long RItemID { get; set; }
        public string Code { get; set; }
        public string Full_Name { get; set; }
        public double Required { get; set; }
        public double IssueQty { get; set; }
        public int IsPending { get; set; }
        public string PlanNo { get; set; }
        public string USName { get; set; }
    }
}
