using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class MetalPartDetails
    {
        public int MepId { get; set; }
        public string MepName { get; set; }
        public string MepDesc { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
