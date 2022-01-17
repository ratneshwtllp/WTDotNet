using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ThicknessDetails
    {
        public int Thid { get; set; }
        public string Thname { get; set; }
        public string Thdesc { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
