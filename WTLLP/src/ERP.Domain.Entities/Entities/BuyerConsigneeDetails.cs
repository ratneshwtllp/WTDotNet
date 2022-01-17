using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class BuyerConsigneeDetails
    {
        public long BuyerConsigneeId { get; set; }
        public long BuyerId { get; set; }
        public long ConsigneeId { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public virtual BuyerDetails BuyerDetails { get; set; }
    }
}
