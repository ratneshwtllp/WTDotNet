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

    // View_NonReturnableOutPass_FItem
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewNonReturnableOutPassFItem
    {
        public int ReturnableId { get; set; } // Returnable_ID
        public int SNo { get; set; } // SNo
        public string OutPassNo { get; set; } // OutPassNo (length: 50)
        public System.DateTime Date { get; set; } // Date
        public string IssueTo { get; set; } // IssueTo (length: 150)
        public int FItemId { get; set; } // FItem_Id
        public int OutPassQty { get; set; } // OutPassQty
        public string ResonForIssue { get; set; } // ResonForIssue (length: 150)
        public string Name { get; set; } // Name (length: 50)
        public string Code { get; set; } // Code (length: 500)
        public string CName { get; set; } // CName (length: 50)
        public string AddressOffice { get; set; } // Address_office
        public string AddressWork { get; set; } // Address_work
        public string SessionYear { get; set; } // SessionYear (length: 50)

        public ViewNonReturnableOutPassFItem()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
