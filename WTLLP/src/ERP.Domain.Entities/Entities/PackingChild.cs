using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class PackingChild
    {    
        public long PackingChildID { get; set; }
        public long PackingID { get; set; }
        public long OrderChildID { get; set; }
        public int CartonNo { get; set; }
        public int CartonID { get; set; }
        public int PackingQty { get; set; }
        public int SNo { get; set; }
        public string Barcode { get; set; }
        public long FitemId { get; set; }
        public long ShoesCartonMasterId { get; set; }
        public int SizeId { get; set; }

        public virtual PackingMaster PackingMaster { get; set; }
    }
}
