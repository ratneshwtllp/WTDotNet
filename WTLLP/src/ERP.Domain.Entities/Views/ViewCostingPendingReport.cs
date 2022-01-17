using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewCostingPendingReport
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string ProductSubCategoryName { get; set; }
        public long ProductSubCategoryID { get; set; }
        public string ProductCategoryName { get; set; }
        public long FitemId { get; set; }
        public string Code { get; set; }
        public string PhotoPath { get; set; }
        public long ProductCategoryID { get; set; }
        public long BuyerId { get; set; }
    }

}

