using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class SplittingMaster
    {
        public long SplittingTid { get; set; }
        public long SplittingId { get; set; }
        public string SplittingNo { get; set; }
        public DateTime SplittingDate { get; set; }
        public long PlanId { get; set; }
        public int? QcOnProcess { get; set; }
        public int? Aproved { get; set; }
        public string Comments { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
    }
}
