using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Domain.Entities
{
    public partial class ProductProcessCharge
    {
        public long ProductProcessChargeId { get; set; }
        public long FitemId { get; set; }
        public long ProcessId { get; set; }
        public int ComponentId { get; set; }
        public double ProcessCharge { get; set; }
        public DateTime EntryDate { get; set; }
        public int UserId { get; set; }
        public int ProductSizeId { get; set; }
    }
}
