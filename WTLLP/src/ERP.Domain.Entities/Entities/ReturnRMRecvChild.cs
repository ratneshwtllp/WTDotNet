using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ReturnRMRecvChild
    {
        public long ReturnRMRecvChildID { get; set; }
        public long ReturnRMRecvID { get; set; }
        public long RitemId { get; set; }
        public double ReturnQty { get; set; }
        public double ReturnSide { get; set; }
        public string Unit { get; set; }
        public int SNo { get; set; }
        public string RMRemark { get; set; }
        public long SupplierId { get; set; }
        public long RackId { get; set; }
        public string LotNo { get; set; }

        public virtual ReturnRMRecv ReturnRMRecv { get; set; }
    }
}
