using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class BrandDetails
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string BrandDesc { get; set; }
        public long BuyerId { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
        
    }
}
