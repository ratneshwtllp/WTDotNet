using System;

namespace ERP.Domain.Entities
{
    public class ViewOrderStatus
    {
        public long BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; }
        public long OrderMasterId { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime MyDeliveryDate { get; set; }
        public int IsShoesOrder { get; set; }
        public int CancelStatus { get; set; }
        public long OrderChildId { get; set; }
        public long FitemId { get; set; }
        public int OrderQty { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int ProductSizeId { get; set; }
        public string ProductSizeName { get; set; }
        public long PlanId { get; set; }
        public DateTime PlanDate { get; set; }
        public string PlanNo { get; set; }
        public long PlanChildId { get; set; }
        public int PlanQty { get; set; }

        public string SizePartyCode { get; set; }
        public string CLName { get; set; }
        public string Unit { get; set; }


        
    }
}
