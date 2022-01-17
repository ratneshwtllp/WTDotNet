using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ProductSizeDetails
    {
        public int ProductSizeId { get; set; }
        public string ProductSizeName { get; set; }
        public string ProductSizeDesc { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
        public int Online_Transfer { get; set; }
    }
}
