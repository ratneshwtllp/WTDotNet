using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class RequestRMIssue
    { 
        public long ReqRMIssueId { get; set; }
        public long ReqRMIssueSNo { get; set; }
        public string ReqRMIssueNo { get; set; }
        public DateTime ReqRMIssueDate { get; set; }
        public long ReqRMId { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public long DepartmentId { get; set; }

        public virtual ICollection<RequestRMIssueChild> RequestRMIssueChild { get; set; }
    }
}
