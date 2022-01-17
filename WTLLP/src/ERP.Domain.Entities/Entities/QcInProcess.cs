using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class QcInProcess
    {
        public int QcProcessId { get; set; }
        public long PlanId { get; set; }
        public int ItemId { get; set; }
        public DateTime QcDate { get; set; }
        public string Remarks { get; set; }
        public DateTime EntryDate { get; set; }
        public int UserId { get; set; }
        public string SessionYear { get; set; }
    }
}
