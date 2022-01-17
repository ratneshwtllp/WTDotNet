using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ShoesCartonChild
    { 
        public long ShoesCartonChildId { get; set; }
        public long ShoesCartonMasterId { get; set; }
        public long OrderChildId { get; set; }
        public long FitemId { get; set; }
        public int SizeId { get; set; } 
        public string Barcode { get; set; }
        public int Qty { get; set; }
        public int SNo { get; set; }

        public virtual ShoesCartonMaster ShoesCartonMaster { get; set; }
    }
}
