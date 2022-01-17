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

    // View_PurchaseVoucher_Report
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewPurchaseVoucherReport
    {
        public string PName { get; set; } // P_Name
        public System.DateTime PVoucherDate { get; set; } // P_Voucher_Date
        public int AccNo { get; set; } // AccNo
        public double TotalAmount { get; set; } // TotalAmount
        public long PVoucherId { get; set; } // P_Voucher_Id
        public long Sid { get; set; } // SID
        public double? TaxTotal { get; set; } // Tax_Total
        public double? Total { get; set; } // Total
        public double? Roundoff { get; set; } // Roundoff
        public double? VoucherAmt { get; set; } // Voucher_Amt
        public string Remark { get; set; } // Remark
        public string AmtInWords { get; set; } // AmtInWords
        public int? SipOrDirect { get; set; } // SIP_or_Direct
        public string Billno { get; set; } // Billno (length: 50)
        public System.DateTime? BillDate { get; set; } // BillDate
        public long? PVoucherSId { get; set; } // P_Voucher_SId
        public string AccName { get; set; } // AccName (length: 100)

        public ViewPurchaseVoucherReport()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>