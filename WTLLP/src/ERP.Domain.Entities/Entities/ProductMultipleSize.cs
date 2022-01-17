using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ProductMultipleSize
    { 
        public long ProductMultipleSizeId { get; set; }
        public long FitemId { get; set; }
        public int SizeId { get; set; }
        public double SizePrice { get; set; }
        public string SizeBarcode { get; set; }
        public string SizePartyCode { get; set; }
        public virtual ProductDetails ProductDetails { get; set; }

    }
}
