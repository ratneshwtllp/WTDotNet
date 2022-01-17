using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class RequestRMIssueChild
    {
        public long ReqRMIssueChildId { get; set; }
        public long ReqRMIssueId { get; set; }
        public long RitemId { get; set; }
        public double IssueQty { get; set; }
        public double IssueSide { get; set; }
        public string Unit { get; set; }
        public int SNo { get; set; }
        public string RMRemark { get; set; }
        public long SupplierId { get; set; }
        public long RackId { get; set; }
        public long ReqRMChildId { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public string LotNo { get; set; }

        public virtual RequestRMIssue RequestRMIssue { get; set; }
    }
}
