using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class FitemQcentry
    {
        public long FitemQcentryId { get; set; }
        public long PlanId { get; set; }
        public long SubWpId { get; set; }
        public DateTime QcDate { get; set; }
        public DateTime EntryDate { get; set; }
        public int QcQty { get; set; }
        public int QcPass { get; set; }
        public int QcReject { get; set; }
        public string QcBy { get; set; }
        public int UserId { get; set; }
        public string SessionYear { get; set; }
    }
}
