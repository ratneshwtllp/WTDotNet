using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewPODelay
    {
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
        public long POID { get; set; }
        public string PONO { get; set; }
        public DateTime PODate { get; set; }
        public DateTime DLDate { get; set; }
        public long GRNID { get; set; }
        public string GRNNO { get; set; }
        public DateTime GRNDate { get; set; }
        public string BillNo { get; set; }
        public string ChallanNo { get; set; }
        public int DelayDay { get; set; }
       
    }
}
