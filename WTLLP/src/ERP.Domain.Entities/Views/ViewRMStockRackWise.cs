namespace ERP.Domain.Entities
{ 
    public class ViewRMStockRackWise
    { 
        public long RowID { get; set; }
        public long RItem_Id { get; set; }
        public string SupplierName { get; set; }
        public string RackNo { get; set; } 
        public string Remark { get; set; }
        public long RackId { get; set; }
        public long SupplierId { get; set; }  
        public double CurrentStock { get; set; }
        public int CostUnit { get; set; }
        public string CostUnitName { get; set; }
        public string CostUnitSName { get; set; }
        public string Code { get; set; }
        public string Full_Name { get; set; }
        public string Title { get; set; }
        public string LotNo { get; set; }
        public double Rate { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
    }
}
