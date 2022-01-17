using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewProductListForProcessCharge
    {
        public long FitemId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long ProductSubCategoryID { get; set; }
        public string ProductSubCategoryName { get; set; }
        public long ProductCategoryID { get; set; }
        public string ProductCategoryName { get; set; }
        public string PhotoPath { get; set; }
        public long ProcessChargeId { get; set; }
        public double KnottingCharge { get; set; }
        public double ToolingCharge { get; set; }
        public double CostingKnottingChargesOfProduct { get; set; }
        public double CostingToolingChargesOfProduct { get; set; }
        public string ProductSizeName { get; set; }
        public int ProductSizeId { get; set; }
        public long ProductMultipleSizeId { get; set; }
    }
}
