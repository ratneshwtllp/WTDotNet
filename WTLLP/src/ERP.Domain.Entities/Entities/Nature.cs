using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class Nature
    {
        public int Nid { get; set; }
        public string NatureName { get; set; }
        public string NatureDesc { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
