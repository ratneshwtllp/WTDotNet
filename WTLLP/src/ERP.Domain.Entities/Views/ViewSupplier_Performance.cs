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

    // View_Supplier_Performance
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewSupplier_Performance
    {
        public string PName { get; set; } // P_Name
        public System.DateTime PoDate { get; set; } // PODate
        public string Pono { get; set; } // PONO (length: 50)
        public System.DateTime DlDate { get; set; } // DLDate
        public long Sid { get; set; } // SID
        public int? BlackListed { get; set; } // Black_Listed
        public string Gipno { get; set; } // GIPNO (length: 20)
        public System.DateTime GipDate { get; set; } // GIPDate
        public int? DayDiffrence { get; set; } // Day_diffrence
        public string CName { get; set; } // CName (length: 50)
        public string AddressOffice { get; set; } // Address_office
        public string AddressWork { get; set; } // Address_work
        public string Phone { get; set; } // Phone (length: 50)

        public ViewSupplier_Performance()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>