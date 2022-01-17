using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewRMSubCategory
    {
        public long SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public string Description { get; set; }

        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string ShortCode { get; set; }
        public int HSNId { get; set; }
        public string HSNCode { get; set; }
    }
}
