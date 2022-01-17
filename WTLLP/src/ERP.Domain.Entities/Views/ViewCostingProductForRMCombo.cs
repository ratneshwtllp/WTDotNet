using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewCostingProductForRMCombo
    {
        public long FitemId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ItemCode { get; set; }
        public long CostingID { get; set; }
        public string ProductSizeName { get; set; }
        public long MasterBelongTo { get; set; }
        public long BelongTo { get; set; }
        public string PhotoPath { get; set; }
        
    }
}
