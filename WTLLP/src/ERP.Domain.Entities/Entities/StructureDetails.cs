using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class StructureDetails
    {
        public int Tsid { get; set; }
        public string Tsname { get; set; }
        public string Tsdesc { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
