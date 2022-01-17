using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ProcessExecutionMaster
    {
        public long PRMasterId { get; set; }
        public long PRSerialNo { get; set; }
        public string PRNo { get; set; }
        public long ContractorID { get; set; }
        public DateTime PRDate { get; set; }
        public int ProcessID { get; set; }
        public string Through { get; set; }
        public long PlanId { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public long PlanChildId { get; set; }
        public double ProcessCharge { get; set; }
        public double ProcessAmount { get; set; }
        public string Remark { get; set; }
        public int IsRead { get; set; }
        public int IsProcessReceive { get; set; }
        public int ProcessFromId { get; set; }
        public int ProcessFromQty { get; set; }
        public int ProcessToQty { get; set; }
        public DateTime EstimatedRecieveDate { get; set; }
        public int IsRMIssue { get; set; }
        public virtual ICollection<ProcessExecutionChild> ProcessExecutionChild { get; set; }
    }
}
