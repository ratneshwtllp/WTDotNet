using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewStockTransferIssue
    {
        public long StockTransferIssueId { get; set; }
        public string StockTransferIssueNo { get; set; }
        public string GPNo { get; set; }
        public string VehicalNo { get; set; }
        public long StockTransferRequestId { get; set; }
        public int TransportId { get; set; }
        public DateTime StockTransferIssueDate { get; set; }
        public DateTime SupplyDate { get; set; }
        public DateTime GPDate { get; set; }
        public long StockTransferIssueChildId { get; set; }
        public long StockTransferRequestChildId { get; set; }

        public long RequestBranchId { get; set; }
        public long RequestToBranchId { get; set; } 

        public int SNo { get; set; }
        public long RitemId { get; set; }
        public double IssueQty { get; set; }
        public string Unit { get; set; }
        public long SupplierId { get; set; }
        public long RackId { get; set; }
        public string LotNo { get; set; }
        public string RackNo { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public string HSNCode { get; set; }
        public int TaxId { get; set; }
        public double  TaxPer { get; set; } 
        public string TaxFullName { get; set; }
        public double  CGSTPer { get; set; }
        public double CGSTAmt { get; set; }
        public double SGSTPer { get; set; }
        public double SGSTAmt { get; set; }
        public double IGSTPer { get; set; }
        public double IGSTAmt { get; set; }
        public double AfterTax { get; set; }
        public double TotalAmount { get ; set; }
        public double TotalIGST { get; set; }
        public double TotalCGST { get; set; }
        public double TotalSGST { get; set; }
        public double TotalAmountAfterTax { get; set; }
        public string AmountInWords { get; set; }



        public string Name { get; set; }
        public string Full_Name { get; set; }
        public long SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string BranchName { get; set; }
        public string RequestToBranchName { get; set; }
        public string CName { get; set; }
        public string AddressOffice { get; set; }
        public string AddressWork { get; set; }
        public string LogoPath { get; set; }

        public double RequestQty { get; set; }
        public string Requestqtyunit { get; set; } 
        public string StockTransferRequestNo { get; set; }
        public DateTime StockTransferRequestDate { get; set; }
        public string RitemRemark { get;set;}
        public string TranportName { get; set; }
        public string SupplierName { get; set; }

        public int UserId { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public string BranchAddress { get; set; }
        public string BranchToAddress { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; } 

        public string StateCode { get; set; }
        public string StateName { get; set; }

        public string BranchTOSateCode { get; set; }
        public string BranchTOStateName { get; set; }
    }
}
