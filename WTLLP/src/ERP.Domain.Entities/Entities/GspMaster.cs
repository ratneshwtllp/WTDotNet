using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class GspMaster
    {
        public long GspId { get; set; }
        public string InvoiceNo { get; set; }
        public string GoodsConsignedFrom { get; set; }
        public string GoodsConsignedTo { get; set; }
        public string MeansOfTransport { get; set; }
        public string OriginCriterion { get; set; }
    }
}
