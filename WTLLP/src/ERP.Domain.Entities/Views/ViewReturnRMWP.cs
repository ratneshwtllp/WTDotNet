using System;

namespace ERP.Domain.Entities
{
    public class ViewReturnRMWP
    {
        public long PlanId { get; set; }
        public string PlanNo { get; set; }
        public long ReturnRMWPID { get; set; }
        public DateTime ReturnDate { get; set; }
        public string RackNo { get; set; }
        public string Code { get; set; }
        public string Full_Name { get; set; }
        public string SubCategoryName { get; set; }
        public long RitemId { get; set; }
        public long SupplierId { get; set; }
        public long RackId { get; set; }
        public double ReturnQty { get; set; }
        public string Comments { get; set; }
        public int CostUnit { get; set; }
        public string CostUnitSName { get; set; }
        public string Title { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
        public long CategoryID { get; set; }
        public long SubCategoryID { get; set; }
        public string LotNo { get; set; }
    }
}
