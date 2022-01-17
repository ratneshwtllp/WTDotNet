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

    // View_PackingIssue
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewPackingIssue
    {
        public string PName { get; set; } // P_Name
        public long? PackingId { get; set; } // Packing_Id
        public long? PackingNo1 { get; set; } // Packing_No
        public string PackingNo { get; set; } // PackingNo (length: 50)
        public System.DateTime? PackingDate { get; set; } // Packing_Date
        public string IssueTo { get; set; } // IssueTo (length: 50)
        public int? BuyerId { get; set; } // Buyer_ID
        public string Remarks { get; set; } // Remarks (length: 50)
        public string SessionYear { get; set; } // Session_Year (length: 50)
        public int? UserId { get; set; } // User_ID
        public string Type { get; set; } // Type (length: 50)
        public string Name { get; set; } // Name (length: 50)
        public int? Qty { get; set; } // Qty
        public string OrderNo { get; set; } // OrderNo (length: 50)

        public ViewPackingIssue()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>