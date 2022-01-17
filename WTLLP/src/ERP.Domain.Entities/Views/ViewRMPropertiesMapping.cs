using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{ 
    public class ViewRMPropertiesMapping
    {
        public long RMMappingID { get; set; }
        public int RMPropertiesID { get; set; }
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string RMPropertiesName { get; set; }
        public int RMPropertiesIsMaster { get; set; }
        public int IsRequired { get; set; }

    }
}
