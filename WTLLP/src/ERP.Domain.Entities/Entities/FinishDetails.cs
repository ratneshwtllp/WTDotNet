using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class FinishDetails
    {
        public int Fid { get; set; }
        public string Fname { get; set; }
        public string Fdesc { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
