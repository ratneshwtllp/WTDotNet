using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ReturnRMRecv
    { 
        public long ReturnRMRecvID { get; set; }
        public long ReturnRMRecvSNo { get; set; }
        public string ReturnRMRecvNo { get; set; }
        public DateTime ReturnRMRecvDate { get; set; }
        public long ReturnRMID { get; set; }
        public string SessionYear { get; set; }
        public int UserID { get; set; }
        public long DepartmentId { get; set; }

        public virtual ICollection<ReturnRMRecvChild> ReturnRMRecvChild { get; set; }
    }
}
