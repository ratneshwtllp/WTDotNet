using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ProcessRecvMaster
    {
        public long PRRecvId { get; set; }
        public long PRRecvSerialNo { get; set; }
        public string PRRecvNo { get; set; }
        public DateTime PRRecvDate { get; set; }
        public long PRMasterId { get; set; }
        public int ProcessID { get; set; }
        public long PlanId { get; set; }
        public string PRRecvRemark { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public int PRRecvQty { get; set; }

        public virtual ICollection<ProcessRecvChild> ProcessRecvChild { get; set; }
    }
}
