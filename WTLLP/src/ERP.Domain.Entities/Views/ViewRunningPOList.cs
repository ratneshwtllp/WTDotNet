using System;

namespace ERP.Domain.Entities
{
    public class ViewRunningPOList
    {
        public long Poid { get; set; }
        public string Pono { get; set; }
        public DateTime Podate { get; set; }
        public string SupplierName { get; set; }
        public long SupplierId { get; set; }
        public long Sid { get; set; }
        public int Complete { get; set; }
    }
}
