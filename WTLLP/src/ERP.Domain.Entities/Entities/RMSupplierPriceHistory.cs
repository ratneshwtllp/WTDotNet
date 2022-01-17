using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class RMSupplierPriceHistory
    {
        public long RMPriceHistoryId { get; set; }
        public long RItem_Id { get; set; }
        public long SupplierId { get; set; }
        public double OldPrice { get; set; }
        public double NewPrice { get; set; }
        public int? UserId { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }

    }
}
