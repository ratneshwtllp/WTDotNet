using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class FinishReceive
    {
        public long Frid { get; set; }
        public DateTime Frdate { get; set; }
        public long PlanId { get; set; }
        public string ReceiveBy { get; set; }
        public string Remark { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
    }
}
