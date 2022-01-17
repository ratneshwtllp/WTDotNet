using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewSupplierPerformanceSumarray
    {
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int POCount { get; set; }
        public int TotalDays { get; set; }
        public int AverageDelay { get; set; }
    }
}
