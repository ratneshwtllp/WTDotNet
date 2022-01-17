using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class SubWorkPlan
    {
        public long SubWpId { get; set; }
        public long WpId { get; set; }
        public int SupervisorId { get; set; }
        public string SubWpQty { get; set; }
        public DateTime SubWpEntryDate { get; set; }
        public int UserId { get; set; }
        public string SessionYear { get; set; }

        public virtual WorkPlanMaster SubWp { get; set; }
    }
}
