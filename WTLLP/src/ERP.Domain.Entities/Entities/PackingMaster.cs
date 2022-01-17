using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class PackingMaster
    {
        public long PackingID { get; set; }
        public long BuyerID { get; set; }
        public string PackingNo { get; set; } 
        public long PackingSerial { get; set; } 
        public DateTime PackingDate { get; set; }
        public int TotalPackingQty { get; set; }
        public int IsWeigh { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserID { get; set; }
        public int TotalCarton { get; set; }

        public virtual ICollection<PackingChild> PackingChild { get; set; } 
        public virtual ICollection<PackingWeightDetails> PackingWeightDetails { get; set; }
        //public virtual InvoiceMaster InvoiceMaster { get; set; }

    }
}
