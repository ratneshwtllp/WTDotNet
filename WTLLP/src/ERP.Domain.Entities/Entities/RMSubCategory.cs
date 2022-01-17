using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class RMSubCategory
    {
        public long SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public string Description { get; set; }
        public long CategoryID { get; set; }
        public int HSNId { get; set; }
    }
}
