using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ReturnRMChild
    {
        public long ReturnRMChildID { get; set; }
        public long ReturnRMID { get; set; }
        public long RitemId { get; set; }
        public double ReturnQty { get; set; }
        public double ReturnSide { get; set; }
        public string Unit { get; set; }
        public int SNo { get; set; }
        public string RMRemark { get; set; }

        public virtual ReturnRM ReturnRM { get; set; }
    }
}
