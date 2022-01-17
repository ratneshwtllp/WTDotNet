using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class EmailSettings
    {  
        public long EmailSettingsID { get; set; }
        public int EmailTypeId { get; set; }
        public string EmailAddress { get; set; }
        public string EmailPassword { get; set; } 
        public string EmailSubject { get; set; }
        public string EmailHeader { get; set; }
        public string EmailFooter { get; set; }
        public string OutGoingServer { get; set; }
        public int Port { get; set; }
        public string BCC { get; set; }
        public string EmailTO { get; set; }
        public string CC { get; set; }
    }
}
