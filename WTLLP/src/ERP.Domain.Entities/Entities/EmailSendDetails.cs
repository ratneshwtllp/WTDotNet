using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class EmailSendDetails
    {  
        public long EmailSendID { get; set; }
        public int EmailTypeId { get; set; }
        public long RecordID { get; set; }
        public string EmailAddress { get; set; }
        public string AttachmentPath { get; set; } 
        public DateTime SendDate { get; set; }
        public int SendStatus { get; set; }
        public string CCEmail { get; set; } 
    }
}
