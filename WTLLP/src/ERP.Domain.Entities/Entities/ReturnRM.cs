using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ReturnRM
    { 
        public long ReturnRMID { get; set; }
        public long ReturnRMSNo { get; set; }
        public string ReturnRMNo { get; set; }
        public DateTime ReturnRMDate { get; set; }
        public int ReturnRMFor { get; set; }
        public string ReturnRMRemark { get; set; }
        public int ReturnRMStatus { get; set; }
        public string SessionYear { get; set; }
        public int UserID { get; set; }
        public long DepartmentId { get; set; }

        public virtual ICollection<ReturnRMChild> ReturnRMChild { get; set; }
    }
}
