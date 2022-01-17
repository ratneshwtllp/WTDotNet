using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class DieDetails
    { 
        public int DieId { get; set; }
        public string DieName { get; set; }
        public string DieNo { get; set; }
        public string DieDesc { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }

        public virtual ICollection<DieFitem> DieFitem { get; set; }
    }
}
