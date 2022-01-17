namespace ERP.Domain.Entities
{ 
    public class ViewRMStock
    { 
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public long SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public int BelongTo { get; set; }
        public long RItem_Id { get; set; }
        public string Code { get; set; }
        public string Full_Name { get; set; }
        public int  CostUnit { get; set; }
        public double CurrentStock { get; set; }
        public string CostUnitName { get; set; }
        public string CostUnitSName { get; set; }
        public string Title { get; set; }
        public double Amount { get; set; }
        public string Name { get; set; }

        public int HSNId { get; set; }
        public string HSNCode { get; set; }
        public int TaxId { get; set; }
        public string TaxFullName { get; set; }
        public double TaxPer { get; set; }

        public double ConversionFactor { get; set; }
        public string SUnit { get; set; }
    }
}
