using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class Stone
    {
        public int Sid { get; set; }
        public string StoneName { get; set; }
        public string StoneDesc { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
