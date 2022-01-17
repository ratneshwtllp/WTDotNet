using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class IssueRMforChangeItem
    {
        public long IssueRMChangeItemID { get; set; }
        public long IssueRMChangeID { get; set; }
        public long RItem_Id { get; set; }
        public double JWQty { get; set; }
        public int SNo { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public virtual IssueRMforChangeMaster IssueRMforChangeMaster { get; set; }
    }
}
