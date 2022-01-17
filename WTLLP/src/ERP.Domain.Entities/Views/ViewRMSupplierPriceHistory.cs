namespace ERP.Domain.Entities
{
  
    public class ViewRMSupplierPriceHistory
    {
        public long RMPriceHistoryId { get; set; }
        public long RItem_Id { get; set; }
        public long SupplierId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string SupplierName { get; set; }
        public double OldPrice { get; set; }
        public double NewPrice { get; set; }
        public System.DateTime EntryDate { get; set; }
        public int UserId { get; set; }
        public string SessionYear { get; set; }
    }

}
