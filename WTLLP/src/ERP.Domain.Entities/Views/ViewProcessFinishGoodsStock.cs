using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewProcessFinishGoodsStock
    {
        public long PRMasterId { get; set; }
        public long PRChildId { get; set; }
        public int ProcessID { get; set; }
        public int ProcessFromQty { get; set; }
        public int ProcessToQty { get; set; }
        public long PlanId { get; set; }
        public string OrderNo { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime OrderDate { get; set; }
        public string Code { get; set; }
        public string Unit { get; set; }
        public string CLName { get; set; }
        public string ProductSizeName { get; set; }
        public long BuyerId { get; set; }

    }
}
