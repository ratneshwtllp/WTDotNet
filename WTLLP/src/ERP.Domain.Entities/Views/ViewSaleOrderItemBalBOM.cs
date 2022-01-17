using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewSaleOrderItemBalBOM
    {
        //FitemId, Name, Code, MasterBelongTo, BelongTo, CostingID, ProductSizeId, ProductSizeName, 
        //CostingRMID, RItemID, RMQty, CWAS, CAfterWAS, RMPrice, RMCAmount, BOMWAS, BOMAfterWas, 
        //RMBAmount, SerialNo, IsComponent, SupplierId, RMCode, Full_Name, SubCategoryID,
        //SubCategoryName, CategoryID, CategoryName, CostUnitName, CostUnitSName, CostUnit, UName, 
        //USName, PUnit, Title, Required, BalanceQtyForWorkPlan, OrderMasterID, OrderNo

        public long OrderMasterID { get; set; }
        public string OrderNo { get; set; }
        public long FitemId { get; set; }
        public string Code { get; set; }
        public int ProductSizeId { get; set; }
        public string ProductSizeName { get; set; }
        public int BalanceQtyForWorkPlan { get; set; }

        public long RItemID { get; set; }
        public string RMCode { get; set; }
        public string Full_Name { get; set; }
        public double Required { get; set; }
        public int IsShoesOrder { get; set; }
        

        //public long FitemId { get; set; }
        //public string Name { get; set; }
        //public string Code { get; set; }
        //public long MasterBelongTo { get; set; }
        //public long BelongTo { get; set; }
        //public long CostingID { get; set; }
        //public int ProductSizeId { get; set; }
        //public string ProductSizeName { get; set; }

        //public long CostingRMID { get; set; }
        //public long RItemID { get; set; }
        //public double RMQty { get; set; }
        //public double CWAS { get; set; }
        //public double CAfterWAS { get; set; }
        //public double RMPrice { get; set; }
        //public double RMCAmount { get; set; }
        //public double BOMWAS { get; set; }
        //public double BOMAfterWas { get; set; }


    }
}
