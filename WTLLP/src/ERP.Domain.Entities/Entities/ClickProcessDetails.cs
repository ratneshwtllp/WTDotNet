using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ClickProcessDetails
    {
        public long ClickId { get; set; }
        public long SoId { get; set; }
        public long JobCardId { get; set; }
        public decimal ClickRate { get; set; }
        public DateTime ClickStart { get; set; }
        public DateTime ClickEnd { get; set; }
        public string ClickDescrpt { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
    }
}
