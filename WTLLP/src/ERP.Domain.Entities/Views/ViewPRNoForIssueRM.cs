using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewPRNoForIssueRM
    {
       
        public long PRMasterId { get; set; }
        public string PRNo { get; set; }
        public int ProcessID { get; set; }
    }
}
