using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class GRNJWRMComsumption
    { 
        public long GRNJWRMComsumptionId { get; set; }
        public long GRNID { get; set; }
        public long IssueRMChangeChildID { get; set; }
        public long RItem_Id { get; set; }
        public double ConsumeQtyFUnit { get; set; }
        public double ConsumeQtySUnit { get; set; }

        public virtual GRNMaster GRNMaster { get; set; }
    }
}
