using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ClosingMaster
    {
        public long ClosingTid { get; set; }
        public long ClosingId { get; set; }
        public string ClosingNo { get; set; }
        public DateTime ClosingDate { get; set; }
        public long PlanId { get; set; }
        public int? QcOnProcess { get; set; }
        public int? Aproved { get; set; }
        public string Comments { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
    }
}
