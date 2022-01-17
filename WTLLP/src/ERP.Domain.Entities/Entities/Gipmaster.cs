using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class Gipmaster
    {
        public long TableId { get; set; }
        public long Gipid { get; set; }
        public string Gipno { get; set; }
        public DateTime Gipdate { get; set; }
        public long Sid { get; set; }
        public string Challanno { get; set; }
        public string Billno { get; set; }
        public string Vehcilno { get; set; }
        public string Drivername { get; set; }
        public string Remark { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public string StoreMis { get; set; }
        public string PoManual { get; set; }
        public int? Verify { get; set; }
    }
}
