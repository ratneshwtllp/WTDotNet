using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewBrandDetails
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string BrandDesc { get; set; }
        public long BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; }
    }
}
