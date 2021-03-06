// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 4.61
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace ERP.Domain.Entities
{

    // View_Bill_Modify
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewBillModify
    {
        public long BillId { get; set; } // Bill_ID
        public long Grnid { get; set; } // GRNID
        public string Pono { get; set; } // PONO (length: 20)
        public long VoucherNo { get; set; } // VoucherNo
        public System.DateTime VoucherDate { get; set; } // VoucherDate
        public string SessionYear { get; set; } // Session_year (length: 20)
        public int UserId { get; set; } // UserID
        public string BillNoFromGrn { get; set; } // BillNo_from_GRN (length: 50)
        public decimal TotalAmt { get; set; } // Total_Amt
        public decimal AmtAfterTax { get; set; } // Amt_After_tax
        public decimal Per { get; set; } // Per
        public decimal Amount { get; set; } // Amount
        public string Gipno { get; set; } // GIPNO (length: 20)
        public System.DateTime GipDate { get; set; } // GIPDate
        public long SupplierId { get; set; } // Supplier_Id
        public string PName { get; set; } // P_Name
        public string FullName { get; set; } // Full_Name
        public long ItemId { get; set; } // Item_Id
        public int TaxId { get; set; } // Tax_ID

        public ViewBillModify()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
