using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ProductMultipleColor
    { 
        public long ProductMultipleColorId { get; set; }
        public long FitemId { get; set; }
        public int ColorId { get; set; } 
         
        public virtual ProductDetails ProductDetails { get; set; }
    }
}
