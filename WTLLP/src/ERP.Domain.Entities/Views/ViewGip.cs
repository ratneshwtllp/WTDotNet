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

    // View_GIP
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewGip
    {
        public long TableId { get; set; } // Table_ID
        public long Gipid { get; set; } // GIPID
        public string Gipno { get; set; } // GIPNO (length: 20)
        public System.DateTime GipDate { get; set; } // GIPDate
        public long Sid { get; set; } // SID
        public string Challanno { get; set; } // Challanno (length: 50)
        public string Billno { get; set; } // Billno (length: 50)
        public string Vehcilno { get; set; } // Vehcilno (length: 50)
        public string Drivername { get; set; } // Drivername (length: 50)
        public string Remark { get; set; } // Remark
        public string SessionYear { get; set; } // Session_year (length: 20)
        public int UserId { get; set; } // UserID
        public string StoreMis { get; set; } // Store_Mis (length: 50)
        public string PoManual { get; set; } // PO_Manual (length: 50)
        public int? Verify { get; set; } // Verify
        public double Qty { get; set; } // Qty
        public int Rmid { get; set; } // RMID
        public int SNo { get; set; } // SNo
        public string Pono { get; set; } // PONO (length: 50)
        public string Unit { get; set; } // Unit (length: 50)
        public string PName { get; set; } // P_Name
        public string Address { get; set; } // Address
        public string CName { get; set; } // CName (length: 50)
        public string AddressOffice { get; set; } // Address_office
        public string AddressWork { get; set; } // Address_work
        public string Phone { get; set; } // Phone (length: 50)
        public string Fax { get; set; } // Fax (length: 50)
        public string Email { get; set; } // Email (length: 100)
        public string Web { get; set; } // Web (length: 100)
        public string PanNo { get; set; } // PANNo (length: 50)
        public string Tin { get; set; } // TIN (length: 50)
        public string IeCode { get; set; } // IECode (length: 50)
        public int? Expr1 { get; set; } // Expr1
        public int? SipQty { get; set; } // SIP_Qty
        public string ItemSuppName { get; set; } // Item_Supp_Name (length: 200)
        public string Name { get; set; } // Name (length: 50)
        public string FullName { get; set; } // Full_Name
        public string Code { get; set; } // Code (length: 50)
        public long? RmCode { get; set; } // RM_Code

        public ViewGip()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>