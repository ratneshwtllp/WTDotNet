using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class IssueRmOtherThanBomMaster
    {
        public long OtherId { get; set; }
        public long OtherSno { get; set; }
        public string OtherNo { get; set; }
        public long? PlanId { get; set; }
        public DateTime? OtherDate { get; set; }
        public string ReceiveBy { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
