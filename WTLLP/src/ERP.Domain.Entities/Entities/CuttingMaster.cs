using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class CuttingMaster
    {
        public long CuttingTid { get; set; }
        public long CuttingId { get; set; }
        public string CuttingNo { get; set; }
        public DateTime CuttingDate { get; set; }
        public long PlanId { get; set; }
        public int? QcOnProcess { get; set; }
        public int? Aproved { get; set; }
        public string Comments { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
    }
}
