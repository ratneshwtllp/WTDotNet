using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class OutPassMaster
    {
        public long TableId { get; set; }
        public long? OutPassId { get; set; }
        public string OutPassNo { get; set; }
        public DateTime? OutPassDate { get; set; }
        public string Comments { get; set; }
        public string OutPassBy { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
