using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class RMProperties
    {
        public int RMPropertiesID { get; set; }      
        public string RMPropertiesName { get; set; }       
        public string RMPropertiesRemark { get; set; }       
        public int RMPropertiesIsPrintOnPo { get; set; }     
        public int RMPropertiesIsMaster { get; set; }       
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public DateTime EntryDate { get; set; }
     
    }
}
