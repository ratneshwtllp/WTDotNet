using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ReceiveWPMaster
    { 
        public long ReceiveWPID { get; set; }
        public long PlanChildId { get; set; }
        public DateTime ReceiveDate { get; set; }
        public int ReceiveQty { get; set; }
        public string Remark{ get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserID { get; set; }
         
    }
}
