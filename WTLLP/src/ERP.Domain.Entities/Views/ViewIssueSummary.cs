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

    // View_IssueSummary
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewIssueSummary
    {
        public double? Quantity { get; set; } // Quantity
        public double Side { get; set; } // Side
        public string IssueNo { get; set; } // Issue_No (length: 50)
        public System.DateTime? IssueDate { get; set; } // Issue_Date
        public string CustOrderNo { get; set; } // Cust_Order_No (length: 50)
        public long? PlanId { get; set; } // Plan_Id
        public string PName { get; set; } // P_Name
        public long? RItemId { get; set; } // RItem_Id
        public long? IssueId { get; set; } // Issue_ID
        public long? IssueSNo { get; set; } // Issue_SNo

        public ViewIssueSummary()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
