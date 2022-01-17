using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewIssuedByDetails
    {
        public int IssuedById { get; set; } 
        public string IssuedByName { get; set; }
        public string IssuedByDesc { get; set; }
        public long BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; }
    }
}
