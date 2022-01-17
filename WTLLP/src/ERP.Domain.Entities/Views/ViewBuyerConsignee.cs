using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewBuyerConsignee
    {
        public long BuyerId { get; set; }
        public string BuyerName { get; set; }
        public long BuyerConsigneeId { get; set; }
        public long ConsigneeId { get; set; }
        public string ConsigneeCode { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneeAddress { get; set; }
        public string BuyerCode { get; set; }
        public string ContactPerson { get; set; }
    }
}
