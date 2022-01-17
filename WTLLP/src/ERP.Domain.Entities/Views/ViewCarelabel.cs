using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewCarelabel
    {
        public long BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; }
        public int CareLabelID { get; set; }
        public string CareLabelLName { get; set; }
        public string Description { get; set; }
        public string PhotoPath { get; set; }
    }
}
