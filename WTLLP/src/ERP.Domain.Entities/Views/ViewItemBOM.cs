using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{ 
    public class ViewItemBOM
    {
        public long CostingRMID { get; set; }
        public long CostingID { get; set; }
        public long RItemID { get; set; }
        public double RMQty { get; set; }
        public double CWAS { get; set; }
        public double CAfterWAS { get; set; }
        public double RMPrice { get; set; }
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
        public long FitemID { get; set; }
        public string ProductCode { get; set; }
        public int Color { get; set; }
        public string Size { get; set; } 
        public string CLName { get; set; }
        public long BelongTo { get; set; } 
        public long ProductSubCategoryID { get; set; }
        public string ProductSubCategoryName { get; set; }
        public long ProductCategoryID { get; set; }
        public string ProductCategoryName { get; set; }
        public string CostUnitName { get; set; }
        public string CostUnitSName { get; set; }
         
    }
}
