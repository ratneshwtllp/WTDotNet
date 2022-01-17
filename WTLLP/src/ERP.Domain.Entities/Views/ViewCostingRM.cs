using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{ 
    public class ViewCostingRM
    { 
        public long CostingRMID { get; set; }
        public long CostingID { get; set; }
        public long RItemID { get; set; }
        public double RMQty { get; set; }
        public double CWAS { get; set; }
        public double CAfterWAS { get; set; }
        
        public double RMCAmount { get; set; }
        public double BOMWAS { get; set; }
        public double BOMAfterWas { get; set; }
        public double RMBAmount { get; set; }
        public int SerialNo { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int CostUnit { get; set; }
        public string Full_Name { get; set; }
        public string Title { get; set; }
        public string CostUnitName { get; set; }
        public string CostUnitSName { get; set; }
        public int IsComponent { get; set; }
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }

        public double RMPrice { get; set; }
        public double CurrentPrice { get; set; }

    }
}
