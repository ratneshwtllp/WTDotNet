using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewLabelDetails
    {
        public int LabelId { get; set; }
        public string LabelName { get; set; }
        public string LabelDesc { get; set; }
        public long BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; }
    }
}
