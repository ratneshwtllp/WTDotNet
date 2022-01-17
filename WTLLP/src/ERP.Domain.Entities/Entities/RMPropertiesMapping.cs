using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class RMPropertiesMapping
    {
        public long RMMappingID { get; set; }
        public long CategoryID { get; set; }
        public long RMPropertiesID { get; set; }
        public int IsRequired { get; set; }
    }
}
