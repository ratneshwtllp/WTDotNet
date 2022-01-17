namespace ERP.Domain.Entities
{
    // ViewProduct
    public class ViewRMSupplier
    { 
        public long RItem_Id { get; set; }
        public string Code { get; set; }
        public string Full_Name { get; set; }
        public long SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
        public long RItemSuppID { get; set; }
        public double Price { get; set; }
        public int MinDays { get; set; }
        public int CID { get; set; }
        public string CName { get; set; }
        public string CSName { get; set; }
        public decimal CRate { get; set; }
        public int SuppUnit { get; set; }
        public string SuppUnitName { get; set; }
        public string SuppUnitSName { get; set; }
        public string Name { get; set; }
        public int PUnit { get; set; }
        public string PUnitName { get; set; }
        public string PUnitSName { get; set; }
        public double CostPrice { get; set; } 
        public string SupplierRMCode { get; set; }

        public int CostUnit { get; set; }
        public double ConversionFactor { get; set; }
        public string CUnitSName { get; set; }
        public string CUnitName { get; set; }


        public double PCSWeight { get; set; }
    }

}
