using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class RequestRMChild
    {
        public long ReqRMChildId { get; set; }
        public long ReqRMId { get; set; }
        public long RitemId { get; set; }
        public double ReqQty { get; set; }
        public double ReqSide { get; set; }
        public string Unit { get; set; }
        public int SNo { get; set; }
        public string RMRemark { get; set; }

        public virtual RequestRM RequestRM { get; set; }
    }
}
