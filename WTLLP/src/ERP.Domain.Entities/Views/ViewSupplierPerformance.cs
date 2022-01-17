using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewSupplierPerformance
    {
        public long POID { get; set; }
        public string PONO { get; set; }
        public DateTime DLDate { get; set; }
        public long SupplierID { get; set; }
        public DateTime GRNDate { get; set; }
        public string SupplierName { get; set; } 
        public string GRNNO { get; set; }
        public int Days { get; set; }
    }
}
