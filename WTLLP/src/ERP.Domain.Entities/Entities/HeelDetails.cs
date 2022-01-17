using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class HeelDetails
    {
        public int HeelId { get; set; }
        public string HeelName { get; set; }
        public string HeelDesc { get; set; } 
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
