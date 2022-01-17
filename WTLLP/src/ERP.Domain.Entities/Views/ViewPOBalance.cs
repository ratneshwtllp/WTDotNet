using System;

namespace ERP.Domain.Entities
{
    public class ViewPOBalance
    {
        public long POChildID { get; set; }
        public long POID { get; set; }
        public int SNo { get; set; }
        public int RItem_Id { get; set; }
        public double Qty { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
        public double FactoryQty { get; set; }
        public string FactoryUnit { get; set; }
        public string RDesc { get; set; }
        public string Pono { get; set; }
        public DateTime Podate { get; set; }
        public DateTime Dldate { get; set; }
        public long Sid { get; set; }
        public short CancelStatus { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
        public double GRNQtySuppUnit { get; set; }
        public double GRNQtyFUnit { get; set; }

        public long BranchID { get; set; }
        public string BranchName { get; set; }
        public string Sessionyear { get; set; }
        public long SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
