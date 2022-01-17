using System;

namespace ERP.Domain.Entities
{
    public class ViewPOForGRN
    {
        public long POChildID { get; set; }
        public long POID { get; set; }
        public int SNo { get; set; }
        public int RItem_Id { get; set; }
        public double Qty { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
        public string RDesc { get; set; }
        public string RMName { get; set; }
        public string RMCode { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryName { get; set; }
        public string Full_Name { set; get; }
        public double ReceivedGRNQty { set; get; }
        public double Balance { set; get; }
        public double ConversionFactor { set; get; }
        public string CostUnit { set; get; }
        public int Batch { set; get; }
        public string SuppUnit { set; get; }
        public short CancelStatus { set; get; }

        public long BranchId { set; get; }
        public string PONO { get; set; }

        public string HSNCode { get; set; }
        public int TaxId { get; set; }
        public double TaxPer { get; set; }
        public string TaxFullName { get; set; }
        public double IGSTPer { get; set; }
        public double IGSTAmt { get; set; }
        public double CGSTPer { get; set; }
        public double CGSTAmt { get; set; }
        public double SGSTPer { get; set; }
        public double SGSTAmt { get; set; }
        public double AmtAfterTax { get; set; }
        public long SID { get; set; }

        public DateTime PODate { get; set; }
    }
}
