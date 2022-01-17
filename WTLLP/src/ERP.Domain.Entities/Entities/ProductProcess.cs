using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ProductProcess
    { 
        public long ProductProcessID { get; set; }
        public long FitemId { get; set; }
        public long ProcessID { get; set; }
        public int SNo { get; set; }
        public DateTime EntryDate { get; set; }
        public int UserId { get; set; }

        public virtual ProductDetails ProductDetails { get; set; }
    }
}
