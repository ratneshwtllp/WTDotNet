using System;

namespace ERP.Domain.Entities
{
    public class ViewGRN
    {
        public long GRNID { get; set; }
        public long SupplierID { get; set; }
        public long POID { get; set; }
        public string GRNNO { get; set; }
        public DateTime GRNDate { get; set; }
        public string BillNo { get; set; }
        public string ChallanNo { get; set; }
        public string VehicleNo { get; set; }
        public string DriverName { get; set; }
        public string Remark { get; set; }
        public long GRNSerial { get; set; }
        public double TotalQty { get; set; }
        public double TotalAmount { get; set; } 
        public int POManual { get; set; }
        public int Verify { get; set; }   
        public int CancelStatus { get; set; }
        public long GRNChildID { get; set; } 
        public long POChildID { get; set; }
        public double GRNQty { get; set; }
        public string LotNo { get; set; }
        public int SNo { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
        public string PONO { get; set; }
        public string Unit { get; set; }
        public long RItem_Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string SessionYear { get; set; }
        public double GRNQtySuppUnit { get; set; }
        public int GRNQtySide { get; set; }
        public string CostUnit { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryName { get; set; }
        public string Full_Name { get; set; }
        public string CName { get; set; }
        public string AddressOffice { get; set; }
        public string SupplierAddress { get; set; }
        public string ContactPerson { get; set; }
        public double Amount { get; set; }
        public double Rate { get; set; }
        public string UserName { get; set; }
        public int TotalQtySide { get; set; }
        public double TotalQtySUnit { get; set; }
        public double FreightAmount { get; set; }
        public double OtherAmoumt { get; set; }
        public double GrandTotal { get; set; }
        public string ReferenceNumber { get; set; }
        public string ShopDetails { get; set; }
        public double ConversionFactor { get; set; }
        public DateTime  BillDate { get; set; }
        public DateTime ChallanDate { get; set; }

        public int PO_JobWork { get; set; }
        public string PO_JW_Number { get; set; }

        public double TotalCGSTAmt { get; set; }
        public double TotalSGSTAmt { get; set; }
        public double TotalIGSTAmt { get; set; }
        public double TotalTaxAmt { get; set; }
        public double TotalAmtAfterTax { get; set; }

        public double FUnitRate { get; set; }
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

        public double POQty { get; set; }
        public double ReceivedGRNQty { get; set; }
    }
}
