using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class LabourCharge
    {
        public long LcId { get; set; }
        public long? ItemId { get; set; }
        public double? LabourRate { get; set; }
        public DateTime? Date { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
