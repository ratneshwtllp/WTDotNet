using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewPODelaySum
    {
        public long SupplierId { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public int TotalDays { get; set; }
        public int NoOfPO { get; set; }
        public int AvrageDelay { get; set; }
    }
}
