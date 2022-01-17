using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class PIChild
    { 
        public long PIChildId { get; set; }
        public long PIMasterId { get; set; }
        public long FitemID { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public int SNo { get; set; }
         
        public virtual PIMaster PIMaster { get; set; }
    }
}  
