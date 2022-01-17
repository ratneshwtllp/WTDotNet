using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewStockTransferRequest
    {
        public long StockTransferRequestId { get; set; }
        public long StockTransferRequestSNo { get; set; }
        public string StockTransferRequestNo { get; set; }
        public DateTime StockTransferRequestDate { get; set; }
        public string RequestRemark { get; set; }
        public long StockTransferRequestChildId { get; set; }
        public long RitemId { get; set; }
        public string RitemRemark { get; set; }
        public double RequestQty { get; set; }
        public string Unit { get; set; }
        public string CName { get; set; }
        public string AddressOffice { get; set; }
        public string AddressWork { get; set; }
        public long RequestBranchId { get; set; }
        public string BranchName { get; set; }
        public string  Address { get; set; }
        public long RequestToBranchId { get; set; }
        public string RequestToBranchName { get; set; }
        public string RequestToAddress { get; set; }
        public string Full_Name { get; set; }
        public string Code { get; set; }
        public long SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }

        public string LogoPath { get; set; }

        public int UserId { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }

        public double CurrentStock { get; set; }
        public double IssueQty { get; set; }








    }
}
