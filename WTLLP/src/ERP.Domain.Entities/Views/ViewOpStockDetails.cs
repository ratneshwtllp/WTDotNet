using System;

namespace ERP.Domain.Entities
{
    public class ViewOpStockDetails
    {
        //Opening Stock 
        public long OpeningStockID { get; set; }
        public long RitemID { get; set; }
        public long SupplierID { get; set; }
        public long RackID { get; set; }
        public double OpeningStockQty { get; set; }
         
        public string Full_Name { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
        public int CostUnit { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryName { get; set; }
        public string RackNo { get; set; }
        public string Remark { get; set; }
        public string CostUnitName { get; set; }
        public string CostUnitSName { get; set; }
        public double Side { get; set; }
        public string LotNo { get; set; }

        public Double  Rate { get; set; }
        public Double Amount { get; set; }
        public DateTime  EntryDate { get; set; }
        public string StoreName { get; set; }

    }
}
