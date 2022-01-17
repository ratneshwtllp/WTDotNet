using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class StoreKeepingMaster
    {  
        public long StoreKeepingID { get; set; }
        public long GRNID { get; set; }
        public string StoreKeepingNO { get; set; }
        public DateTime StoreKeepingDate { get; set; }
        public string StoreKeepingBY { get; set; }
        public string Remark { get; set; }
        public long StoreKeepingSerial { get; set; }
        public int ReadForReserve { get; set; }
        public DateTime EntryDate { get; set; } 
        public string SessionYear { get; set; }
        public int UserID { get; set; }

        public virtual ICollection<StoreKeepingChild> StoreKeepingChild { get; set; }
    }
}
