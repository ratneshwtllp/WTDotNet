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

    // View_PurchaseVoucher
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewPurchaseVoucher
    {
        public string GroupName { get; set; } // Group_Name (length: 100)
        public string PName { get; set; } // P_Name
        public long Gipid { get; set; } // GIPID
        public string Gipno { get; set; } // GIPNO (length: 20)
        public System.DateTime GipDate { get; set; } // GIPDate
        public long Sid { get; set; } // SID
        public string Challanno { get; set; } // Challanno (length: 50)
        public string Billno { get; set; } // Billno (length: 50)
        public string Vehcilno { get; set; } // Vehcilno (length: 50)
        public string Drivername { get; set; } // Drivername (length: 50)
        public string Remark { get; set; } // Remark
        public string StoreMis { get; set; } // Store_Mis (length: 50)
        public string PoManual { get; set; } // PO_Manual (length: 50)
        public int? Verify { get; set; } // Verify
        public string RmName { get; set; } // RMName (length: 500)
        public double? Qty { get; set; } // Qty
        public string AccName { get; set; } // AccName (length: 100)
        public string Unit { get; set; } // Unit (length: 50)
        public int? SNo { get; set; } // SNo
        public string Pono { get; set; } // PONO (length: 50)
        public int? SipQty { get; set; } // SIP_Qty
        public string ItemSuppName { get; set; } // Item_Supp_Name (length: 200)
        public decimal? Rate { get; set; } // Rate
        public int? Rmid { get; set; } // RMID
        public int? Rid { get; set; } // RID
        public int? AccNo { get; set; } // AccNo
        public int? SuppBuyerEmp { get; set; } // Supp_Buyer_EMP
        public int? SuppBuyerEmpId { get; set; } // Supp_Buyer_EMP_ID

        public ViewPurchaseVoucher()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
