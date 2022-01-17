using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class SipMaster
    {
        public long SipId { get; set; }
        public string SipNo { get; set; }
        public string Gipno { get; set; }
        public DateTime SipDate { get; set; }
        public string SipBy { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public string SipRemark { get; set; }
    }
}
