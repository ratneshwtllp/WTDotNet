namespace ERP.Domain.Entities
{
    public partial class ViewIssueRMForChange
    {
        public long IssueRMChangeID { get; set; } 
        public System.DateTime IssueRMChangeDate { get; set; }
        public long IssueRMChangeSNo { get; set; } 
        public string  IssueRMChangeNo { get; set; } 
        public long RItem_Id { get; set; }
        public string Name { get; set; } 
        public string Code { get; set; } 
        public double  IssueQty { get; set; } 
        public long RackId { get; set; }   
        public string RackNo { get; set; } 
        public long SupplierId { get; set; } 
        public string  SupplierName { get; set; } 
        public int Side { get; set; } 
        public string  Comments { get; set; }
        public string  USName { get; set; }
        public long IssueRMChangeChildID { get; set; }
        public int BelongTo { get; set; }
        public string SubCategoryName { get; set; }
        public string CName { get; set; }
        public string AddressOffice { get; set; }
        public string GSTN { get; set; }
        public string AddressWork { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public int IsAddIn_JWOutBal { get; set; }
        public string HSNCode { get; set; }
        public string TaxFullName { get; set; }
        public string LotNo { get; set; }

        public int TaxId { get; set; }
        public double TaxPer { get; set; }
        public double CGSTPer { get; set; }
        public double CGSTAmt { get; set; }
        public double SGSTPer { get; set; }
        public double SGSTAmt { get; set; }
        public double IGSTPer { get; set; }
        public double IGSTAmt { get; set; }
        public double AfterTax { get; set; }

        public double TotalAmount { get; set; }
        public double TotalIGST { get; set; }
        public double TotalCGST { get; set; }
        public double TotalSGST { get; set; }
        public double TotalAmountAfterTax { get; set; }
        public string AmountInWords { get; set; }

        public double SQty { get; set; }
        public string SUnit { get; set; }

        public double ConsumeQtyF { get; set; }
        public double ConsumeQtyS { get; set; }
    }
}
