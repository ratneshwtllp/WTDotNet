using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Domain.Entities
{
    public partial class ViewDashBoardOrderToBeDeliver
    {
        public long BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; }
        public long OrderMasterID { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime MyDeliveryDate { get; set; }
        public int IsShoesOrder { get; set; }
        public int NoOfDaysLeftToDelivery { get; set; }
    }
}
