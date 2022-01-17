using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class RMCategory
    {
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string ShortCode { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
    }
}
