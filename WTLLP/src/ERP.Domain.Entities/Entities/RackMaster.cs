using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class RackMaster
    {
        public long RackId { get; set; }
        public string RackNo { get; set; }
        public long RackSNo { get; set; }
        public string LineNo { get; set; }
        public string LineCode { get; set; }
        public long LineSNo { get; set; } 
        public string Remark { get; set; } 
        public long StoreId { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
