namespace ERP.Domain.Entities
{
    // ViewProduct
    public class ViewProduct
    {
        public long ProductCategoryID { get; set; }
        public string ProductCategoryName { get; set; }
        public string ProductShortCode { get; set; }
        public long ProductSubCategoryID { get; set; }
        public string ProductSubCategoryName { get; set; }
        public string Description { get; set; } // Description
        public long ProductStartWith { get; set; }
        public string SubCatShortCode { get; set; }
        public long SubCatStartWith { get; set; }
        public long Belongto { get; set; }
        public int CatLevel { get; set; }
        public string SubCatImagePath { get; set; }
    }
}
