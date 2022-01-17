using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewHandTag
    {
        public long BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; }
        public int HandTagID { get; set; }
        public string HandTagName { get; set; }
        public string Description { get; set; }
        public string SessionYear { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
