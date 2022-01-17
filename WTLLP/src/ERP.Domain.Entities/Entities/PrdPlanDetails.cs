using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class PrdPlanDetails
    {
        public long PrdPlanId { get; set; }
        public long SoId { get; set; }
        public long JobCardId { get; set; }
        public DateTime PrdPlanDate { get; set; }
        public DateTime ClickDate { get; set; }
        public DateTime SkivingDate { get; set; }
        public DateTime ReceiveDate { get; set; }
        public string PlanPreparedBy { get; set; }
        public string PlanRemark { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public DateTime SampleDate { get; set; }
        public DateTime LeatherDate { get; set; }
        public DateTime FittingDate { get; set; }
    }
}
