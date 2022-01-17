namespace ERP.Domain.Entities
{
    public class ViewUnusedRMList
    {
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public long SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public long RItem_Id { get; set; }
        public string Code { get; set; }
        public string Full_Name { get; set; }
        public string Title { get; set; }
    }
}
