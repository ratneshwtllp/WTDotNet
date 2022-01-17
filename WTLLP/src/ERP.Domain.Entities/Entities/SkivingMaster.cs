using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class SkivingMaster
    {
        public long SkivingTid { get; set; }
        public long SkivingId { get; set; }
        public string SkivingNo { get; set; }
        public DateTime SkivingDate { get; set; }
        public long PlanId { get; set; }
        public int? QcOnProcess { get; set; }
        public int? Aproved { get; set; }
        public string Comments { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
    }
}
