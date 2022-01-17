using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewSaleOrderWPStatus
    {
        public long OrderMasterID { get; set; }
        public string OrderNo { get; set; }
        public long BuyerId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int CancelStatus { get; set; }
        public DateTime MyDeliveryDate { get; set; }
        public int IsShoesOrder { get; set; }
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; }
        public long OrderChildID { get; set; }
        public long FitemId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Qty { get; set; }
        public string ProductSizeName { get; set; }
        public int ProductSizeId { get; set; }
        public long PlanId { get; set; }
        public string PlanNo { get; set; }
    }
}
