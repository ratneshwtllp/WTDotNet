using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class SaleOrderMaster
    {
        public long OrderMasterID { get; set; }
        public string OrderNo { get; set; }
        public long  BuyerId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        public string Remark { get; set; }
        public int TotalQty { get; set; }
        public double  TotalAmount { get; set; }
        public int CancelStatus { get; set; }
        public string SessionYear { get; set; }
        public int UserID { get; set; }
        public DateTime EntryDate { get; set; }

        public int IssuedById { get; set; }
        public string RefrenceNo { get; set; }
        public DateTime MyDeliveryDate { get; set; }
        public DateTime OrderEntryDate { get; set; }
        public DateTime PRDApprovalDate { get; set; }
        public DateTime PRDStartDate { get; set; }
        public DateTime ShipmentSampleDate { get; set; }
        public string FOBCIF { get; set; }
        public int TransportId { get; set; }
        public string TransportCommments { get; set; }

        public int MultipleTransport { get; set; } 
        public int MultipleDeliveryDate { get; set; }

        public DateTime CancelDate { get; set; }
        public int CancelByUserId { get; set; }
        public string CancelRemark { get; set; }
        public int IsShoesOrder { get; set; }
         
        public virtual ICollection<SaleOrderChild > SaleOrderChild { get; set; }
    }
}
