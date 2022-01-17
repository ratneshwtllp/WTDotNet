using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewPackingMaster
    {
        public long PackingID { get; set; }
        public long BuyerID { get; set; }
        public string PackingNo { get; set; } 
        public long PackingSerial { get; set; } 
        public DateTime PackingDate { get; set; }
        public int TotalPackingQty { get; set; }
        public int IsWeigh { get; set; }
        public string BuyerName { get; set; }
        public int CurrencyID { get; set; }
        public string CName { get; set; }
        public string CSName { get; set; }
        public int TotalCarton { get; set; }
    }
}
