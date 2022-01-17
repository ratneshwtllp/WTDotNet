using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class IssueMaster
    { 
        public long IssueID { get; set; }
        public long IssueSNo { get; set; }
        public string IssueNo { get; set; }
        public long PlanId { get; set; }
        public DateTime IssueDate { get; set; }
        public string IssueBy { get; set; }
        public string ReceiveBy { get; set; }
        public int ExtraIssue { get; set; }
        public string Comments { get; set; }
        public int PCKIssue { get; set; }
        public int CancelStatus { get; set; }
        public string SessionYear { get; set; }
        public int UserID { get; set; }

        public long PRMasterId { get; set; } // for Process/Token
        public string PRNo { get; set; }
        public string ProcessName { get; set; }

        public virtual ICollection<IssueChild> IssueChild { get; set; }
    }
}
