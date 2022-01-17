using System;

namespace ERP.Domain.Entities
{
    public class ViewOrderBOMSUM
    {  
        //public long RowID { get; set; }
        public long RItemID { get; set; }
        public long OrderMasterID { get; set; } 
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string RMCode { get; set; }
        public string Full_Name { get; set; }
        //public double RMQty { get; set; } 
        //public int BOMDisplayOrder { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public double Required { get; set; }
        public int CostUnit { get; set; }
        public string Title { get; set; } 
        public string CostUnitName { get; set; }
        public string CostUnitSName { get; set; }
        public int PUnit { get; set; }
        public string PUnitName { get; set; }
        public string PUnitSName { get; set; }
        public double CurrentStock { get; set; }
        public double ReserveStock { get; set; }
        public DateTime MyDeliveryDate { get; set; }
        public string BuyerCode { get; set; }
    }
}
