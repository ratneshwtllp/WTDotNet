using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ReadymadeReceiving
    {
        public long ReceiveId { get; set; }
        public long ReceiveSno { get; set; }
        public string ReceiveNo { get; set; }
        public DateTime ReceiveDate { get; set; }
        public double ReceiveQty { get; set; }
        public string ReceiveBy { get; set; }
        public long PlanId { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public int ReadymadeOrJobwork { get; set; }
        public long SubWpId { get; set; }
        public DateTime EntryDate { get; set; }
        public int SipStatus { get; set; }
        public DateTime SipDate { get; set; }
        public int SipUser { get; set; }
        public int? SipQty { get; set; }
    }
}
