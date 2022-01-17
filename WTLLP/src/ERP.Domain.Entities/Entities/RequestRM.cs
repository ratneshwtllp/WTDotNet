using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class RequestRM
    { 
        public long ReqRMId { get; set; }
        public long ReqRMSNo { get; set; }
        public string ReqRMNo { get; set; }
        public DateTime ReqRMDate { get; set; }
        public int ReqRMFor { get; set; }
        public string ReqRMRemark { get; set; }
        public int ReqRMStatus { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public long DepartmentId { get; set; }

        public virtual ICollection<RequestRMChild> RequestRMChild { get; set; }
    }
}
