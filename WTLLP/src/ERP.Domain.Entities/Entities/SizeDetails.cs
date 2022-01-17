using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class SizeDetails
    {
        public int SizeId { get; set; }
        public string SizeName { get; set; }
        public string SizeDesc { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
