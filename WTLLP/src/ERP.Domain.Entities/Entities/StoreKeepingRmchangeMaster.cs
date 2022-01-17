using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class StoreKeepingRmchangeMaster
    {
        public long SkRmChangeId { get; set; }
        public DateTime SkRmChangeDate { get; set; }
        public long InpassTableId { get; set; }
        public string SkBy { get; set; }
        public string SkRemark { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
    }
}
