using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class InpassMaster
    {
        public long InpassTableId { get; set; }
        public long InpassId { get; set; }
        public string InpassNo { get; set; }
        public DateTime InpassDate { get; set; }
        public string Comments { get; set; }
        public string InpassBy { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public long IssueId { get; set; }
    }
}
