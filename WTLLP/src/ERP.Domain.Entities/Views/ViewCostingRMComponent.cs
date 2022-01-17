using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{ 
    public class ViewCostingRMComponent
    { 
        public long CostingRMComponentID { get; set; }
        public long CostingID { get; set; }
        //public long CostingRMID { get; set; }
        public long CompID { get; set; }
        public long RitemId { get; set; }

        public string Comp_Name { get; set; }
        public double Length { get; set; }
        public double Breadth { get; set; }
        public double Height { get; set; }
        public int CompQty { get; set; }
        public double Area { get; set; }
        public int SerialNo { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        //public double RMQty { get; set; }
        public long SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int CostUnit { get; set; }
        public string UName { get; set; }
        public string USName { get; set; }

        public int CompGroupId { get; set; }
        public string Remark { get; set; }
        public string Photo { get; set; }
        public string CompGroupName { get; set; }
        public double RMQty { get; set; }
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public double CWAS { get; set; }
        public double BOMWAS { get; set; }
        public double CostPrice { get; set; }

        public int ProcessID { get; set; }
    }
}
