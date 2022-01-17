using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ReturnRMWP
    {
        public long ReturnRMWPID { get; set; }
        public DateTime ReturnDate { get; set; }
        public long PlanId { get; set; }
        public long RitemId { get; set; }
        public long SupplierId { get; set; }
        public long RackId { get; set; }
        public double ReturnQty { get; set; }
        public string Comments { get; set; }
        public string LotNo { get; set; }
    }
}
