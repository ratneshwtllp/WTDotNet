using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class SkiveingDetails
    {
        public long SkiveId { get; set; }
        public long SoId { get; set; }
        public long JobCardId { get; set; }
        public decimal SkiveRate { get; set; }
        public DateTime SkiveStart { get; set; }
        public DateTime SkiveEnd { get; set; }
        public string SkiveDescrpt { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
