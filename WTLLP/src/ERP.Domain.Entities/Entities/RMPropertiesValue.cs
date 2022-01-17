using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class RMPropertiesValue
    {
        public long RMPropertiesValueID { get; set; }        
        public int RMPropertiesID { get; set; }              
        public string RMPropertiesValueName { get; set; }
        public string RMPropertiesValueRemark { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public DateTime EntryDate { get; set; }        
    }
}
