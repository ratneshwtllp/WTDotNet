using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ProcessRecvChild  //PRCChild
    {
        public long PRRecvChildId { get; set; }
        public long PRRecvId { get; set; }
        public long PRChildId { get; set; }
        public long FitemId { get; set; }
        public int SizeId { get; set; }
        public int Comp_Id { get; set; }
        public int Comp_RecvQty { get; set; }
        public int SNo { get; set; }
        public long PlanChildId { get; set; }

        public virtual ProcessRecvMaster ProcessRecvMaster { get; set; }
    }
}
