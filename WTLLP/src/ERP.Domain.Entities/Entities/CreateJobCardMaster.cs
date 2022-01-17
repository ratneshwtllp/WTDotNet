using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class CreateJobCardMaster
    {
        public long JobCardId { get; set; }
        public long JobCardNo { get; set; }
        public long SoId { get; set; }
        public int ContrId { get; set; }
        public DateTime ReqDate { get; set; }
        public DateTime DeliDate { get; set; }
        public decimal JobPrice { get; set; }
        public string CheckedBy { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
