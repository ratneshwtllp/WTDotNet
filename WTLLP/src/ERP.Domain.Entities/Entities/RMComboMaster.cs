using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class RMComboMaster
    {
        public long RMComboID { get; set; }
        public string ComboName { get; set; }
        public DateTime ComboDate { get; set; }
        public DateTime EntryDate { get; set; }
        public int UserId { get; set; }

        public virtual ICollection<RMComboChild> RMComboChild { get; set; }
    }
}
