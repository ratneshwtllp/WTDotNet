using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class WaxingMaster
    {
        public long WaxingTid { get; set; }
        public long WaxingId { get; set; }
        public string WaxingNo { get; set; }
        public DateTime WaxingDate { get; set; }
        public long PlanId { get; set; }
        public int? QcOnProcess { get; set; }
        public int? Aproved { get; set; }
        public string Comments { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
    }
}
