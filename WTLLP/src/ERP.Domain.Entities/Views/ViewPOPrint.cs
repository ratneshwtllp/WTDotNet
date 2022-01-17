using System;

namespace ERP.Domain.Entities
{
    public class ViewPOPrint
    {
        public long Poid { get; set; }
        public string Pono { get; set; }
        public DateTime Podate { get; set; }
        public DateTime Dldate { get; set; }
        public long Sid { get; set; }
        public string Mode { get; set; }
        public string Sref { get; set; }
        public string Orf { get; set; }
        public string Dispatch { get; set; }
        public string Destination { get; set; }
        public string Terms { get; set; }
        public string Remark { get; set; }
        //public double Amount { get; set; }
        public string Amtwords { get; set; }
        public string SessionYear { get; set; }
        public long UserId { get; set; }
        public int? PoAgainstOrder { get; set; }
        public int? IssuedBy { get; set; }
        public long? OrderId { get; set; }
        public long? ItemId { get; set; }
        public string ItemCode { get; set; }
        public int? ChangeDilervery { get; set; }
        public DateTime? NewDate { get; set; }
        public string ReasonForChange { get; set; }
        public short? CancelStatus { get; set; }
        public string ReasonForModify { get; set; }
        public string MultipleOrders { get; set; }
        public int POSerial { get; set; }

        public double RMTotal { get; set; }
        public double TaxAmount { get; set; }
        public double SH { get; set; }
        public double Other { get; set; }
        public double RoundOff { get; set; }
        public double NetAmount { get; set; }

        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierAddress { get; set; }

        public string CName { get; set; }
        public string AddressOffice { get; set; }
        public string AddressWork { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public string CSTT { get; set; }
        public string UPTT { get; set; }
        public string TIN { get; set; }
        public string PANNo { get; set; }

        public long SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }


        public long POChildID { get; set; }
        public int SNo { get; set; }
        public int RItem_Id { get; set; }
        public double Qty { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
        public string RDesc { get; set; }

        public string Full_Name { get; set; }
        public string Code { get; set; }
        public string SuppCode { get; set; }

        public string ContactPerson { get; set; }
        public string SupplierPhoneNo { get; set; }
        public string SupplierMobileNo { get; set; }
        public string SupplierFaxNo { get; set; }
        public string SupplierEmail { get; set; }
        public string SupplierPANNo { get; set; }
        public string SupplierCSTNo { get; set; }
        public string SupplierUPTTNo { get; set; }
        public string SupplierTINNo { get; set; }
        public double Freight { get; set; }

        public int CID { get; set; }
        public string CSName { get; set; }
        public string POFooter1 { get; set; }
        public string POFooter2 { get; set; }
        public int EmailStatus { get; set; }
        public DateTime EmailDate { get; set; }
        public long CategoryID { get; set; }

        public string HSNCode { get; set; }
        public double TaxPer { get; set; }
        public string TaxFullName { get; set; }

        public double IGSTPer { get; set; }
        public double IGSTAmt { get; set; }
        public double CGSTPer { get; set; }
        public double CGSTAmt { get; set; }
        public double SGSTPer { get; set; }
        public double SGSTAmt { get; set; }
        public double AmtAfterTax { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string SupplierGSTN { get; set; }
        public string CompanyGSTN { get; set; }
        public string PaymentTerm { get; set; }
        public string DeliveryTerm { get; set; }
        public string Transport { get; set; }
        public string Declaration { get; set; }
        public string ForAnyQuery { get; set; }
        public double TotalIGST { get; set; }
        public double TotalCGST { get; set; }
        public double TotalSGST { get; set; }
        public double TotalAmtAfterTax { get; set; }
        public int SupplierStateId { get; set; }
        public int BranchStateId { get; set; }
        public string SupplierRMCode { get; set; }

        public double FactoryQty { get; set; }
        public string FactoryUnit { get; set; }
        public double ConversionFactor { get; set; }

        public string Title { get; set; }
        public long BuyerId { get; set; }
        public string BuyerName { get; set; }

        public long BranchID { get; set; }
        public DateTime EntryDate { get; set; }

        public int Complete { get; set; }

        public double PCSWeightGrm { get; set; }
        public double PCSTotalWeightKg { get; set; }
        
    }
}
