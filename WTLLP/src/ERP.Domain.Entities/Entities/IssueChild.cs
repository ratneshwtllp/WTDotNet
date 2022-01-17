using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class IssueChild
    {
        public long IssueChildID { get; set; }
        public long IssueID { get; set; }
        public double IssueQty { get; set; }
        public double ReqQty { get; set; }

        public long RitemId { get; set; }
        public long RackId { get; set; }
        public long SupplierId { get; set; }
        public double Side { get; set; }
        public int SNo { get; set; }
        public string LotNo { get; set; }
        public virtual IssueMaster IssueMaster { get; set; }
    }
}
