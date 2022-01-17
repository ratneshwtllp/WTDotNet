using System;

namespace ERP.Domain.Entities
{
    public class ViewOrderBOM
    { 
        //public long RowID { get; set; }
        public long RItemID { get; set; }
        public long OrderMasterID { get; set; }
        public long FitemId { get; set; }
        public string Name { get; set; }
        public string  Code { get; set; }
        public int Qty { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string RMCode { get; set; }
        public string Full_Name { get; set; }
        public double RMQty { get; set; }
        public double CWAS { get; set; }
        public double CAfterWAS { get; set; }
        public double RMPrice { get; set; }
        public double RMCAmount { get; set; }
        public double BOMWAS { get; set; }
        public double BOMAfterWas { get; set; }
        public double RMBAmount { get; set; }
        public int SerialNo { get; set; }
        //public int BOMDisplayOrder { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public double Required { get; set; }
        public int CostUnit { get; set; }
        public int PUnit { get; set; }

        public int ProductSizeId { get; set; }
        public string ProductSizeName { get; set; }

    }
}
