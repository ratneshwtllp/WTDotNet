using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class MessageForSend
    {
        public long MsgId { get; set; }
        public DateTime MsgDate { get; set; }
        public int UserId { get; set; }
        public string MobileNo { get; set; }
        public int MessageMenuId { get; set; }
        public int MsgStatus { get; set; }
        public string Message { get; set; }
    }
}
