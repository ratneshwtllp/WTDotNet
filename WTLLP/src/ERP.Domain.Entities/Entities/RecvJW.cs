using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class RecvJW
    {
        public long RecvJWID { get; set; }
        public DateTime RecvJWDate { get; set; }
        public long IssueRMChangeItemID { get; set; }
        public long RItemId { get; set; }
        public long SupplierId { get; set; }
        public long RackId { get; set; }
        public double RecvJWQty { get; set; }
        public string RefrenceNo { get; set; }
    }
}
