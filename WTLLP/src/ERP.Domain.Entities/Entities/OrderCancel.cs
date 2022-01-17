using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class OrderCancel
    {
        public long OrderCancelId { get; set; }
        public long OrderId { get; set; }
        public DateTime CancelDate { get; set; }
        public string CancelReason { get; set; }
        public int UserId { get; set; }
        public string SessionYear { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
