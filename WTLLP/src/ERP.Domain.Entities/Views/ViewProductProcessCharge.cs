using System;
namespace ERP.Domain.Entities
{
    public partial class ViewProductProcessCharge
    {
        public long FitemId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int ProductSizeId { get; set; }
        public string ProductSizeName { get; set; }
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public double ProcessCharge { get; set; }
        public long ProductProcessChargeId { get; set; }
    }
}

