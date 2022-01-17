namespace ERP.Domain.Entities
{ 
    public class ViewBelowMinimumLevel
    {
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public long SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public int BelongTo { get; set; }
        public long RItem_Id { get; set; }
        public string Code { get; set; }
        public string Full_Name { get; set; }
        public int CostUnit { get; set; }
        public double CurrentStock { get; set; }
        public string CostUnitName { get; set; }
        public string CostUnitSName { get; set; }
        public string Title { get; set; }
        public double Min_Stock { get; set; }
        public double Max_Stock { get; set; }
    }
}
