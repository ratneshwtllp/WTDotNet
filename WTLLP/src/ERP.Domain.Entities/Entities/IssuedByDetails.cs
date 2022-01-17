using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class IssuedByDetails
    {
        public int IssuedById { get; set; } 
        public string IssuedByName { get; set; }
        public string IssuedByDesc { get; set; }
        public long BuyerId { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
