using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class LabelDetails
    {
        public int LabelId { get; set; }
        public string LabelName { get; set; }
        public string LabelDesc { get; set; }
        public long BuyerId { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
        
    }
}
