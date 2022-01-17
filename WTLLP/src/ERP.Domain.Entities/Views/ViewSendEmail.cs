using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewSendEmail
    {  
        public long EmailSettingsID { get; set; }
        public string EmailAddress { get; set; }
        public string EmailPassword { get; set; } 
        public string EmailSubject { get; set; }
        public string EmailHeader { get; set; }
        public string EmailFooter { get; set; }
        public string OutGoingServer { get; set; }
        public int Port { get; set; }
        public string BCC { get; set; }
        public string EmailTO { get; set; }

        //EmailType Table
        public int EmailTypeId { get; set; }
        public string EmailTypeName { get; set; }

        //Email Send Details
        public long EmailSendID { get; set; } 
        public long RecordID { get; set; }
        public string SendToEmailAddress { get; set; }
        public string AttachmentPath { get; set; }
        public DateTime SendDate { get; set; }
        public int SendStatus { get; set; }
        public string CCEmail { get; set; }
        public string CC { get; set; }
    }
}
