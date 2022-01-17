using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class DieFitem
    {
        public long DieFitemId { get; set; }
        public int DieId { get; set; }
        public long FitemId { get; set; }

        public virtual DieDetails DieDetails { get; set; }
    }
}
