using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class WorkPlanMaster
    {
        public long PlanId { get; set; }
        public long OrderId { get; set; }
        public long ContractorID { get; set; }
        public long BuyerID { get; set; }
        public DateTime PlanDate { get; set; }
        public string PlanBy { get; set; }
        public string PlanNo { get; set; }
        public int PlanSNo { get; set; }
        public string Comments { get; set; }
        public int PlanFor { get; set; }
        public long ItemId { get; set; }
        public int PlanQty { get; set; }
        public int SupervisorId { get; set; }
        public string CareLabelNo { get; set; }
        public DateTime DiliveryDatePlan { get; set; }
        public DateTime ApprovalDate { get; set; }
        public DateTime ShipmentDate { get; set; }
        public int CancelStatus { get; set; }
        public DateTime CancelDate { get; set; }
        public DateTime EntryDate { get; set; }
        public int CancelUser { get; set; }
        public string CancelRemark { get; set; }
        public string EditRemark { get; set; }
        public DateTime CreateWpDate { get; set; }
        public int PrintWithImage { get; set; }
        public int Order_Sample { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public int IsPending { get; set; }

        public virtual ICollection<WorkPlanChild> WorkPlanChild { get; set; }
    }
}
