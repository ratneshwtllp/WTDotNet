using System;

namespace ERP.Domain.Entities
{
    public class ViewSaleOrder
    {
        public long OrderMasterID { get; set; }
        public string OrderNo { get; set; }
        public long BuyerId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
      
        public string Remark { get; set; }
        public int TotalQty { get; set; }
        public double TotalAmount { get; set; }
        public int CancelStatus { get; set; }
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; }
        public string RefrenceNo { get; set; }
        public string IssuedByName { get; set; }
        public int IssuedById { get; set; }
        public int TransportId { get; set; }
        public string  TranportName { get; set; }
        public DateTime MyDeliveryDate { get; set; }
        public DateTime OrderEntryDate { get; set; }
        public DateTime PRDApprovalDate { get; set; }
        public DateTime PRDStartDate { get; set; }
        public DateTime ShipmentSampleDate { get; set; }
        public string FOBCIF { get; set; }
        public string TransportCommments { get; set; }
        public int MultipleDeliveryDate { get; set; }
        public int MultipleTransport { get; set; }
        public int IsShoesOrder { get; set; }


        //public string TransportName { get; set; }
        //public DateTime ShippingDate { get; set; }
        //public string TrackingNo { get; set; }
        //public string ShippingRemark { get; set; }
        //public long OrderShippingID { get; set; }
    }
}
