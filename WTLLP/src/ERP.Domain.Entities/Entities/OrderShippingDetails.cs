using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class OrderShippingDetails
    {
        public long OrderShippingID { get; set; }
        public long Order_ID { get; set; }
        public string TransportName { get; set; }
        public DateTime ShippingDate { get; set; }
        public string TrackingNo { get; set; }
        public string Remark { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
