using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class MessageForRmfeed
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public int MessageFrom { get; set; }
        public DateTime MessageDate { get; set; }
        public string MessageText { get; set; }
        public short MessageStatus { get; set; }
        public short ReadByOrder { get; set; }
        public DateTime DoneDate { get; set; }
    }
}
