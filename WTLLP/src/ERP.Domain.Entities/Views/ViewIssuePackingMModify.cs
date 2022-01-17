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

    // View_IssuePackingMModify
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewIssuePackingMModify
    {
        public long IssueId { get; set; } // Issue_ID
        public long IssueSNo { get; set; } // Issue_SNo
        public string IssueNo { get; set; } // Issue_No (length: 50)
        public long? PlanId { get; set; } // Plan_Id
        public System.DateTime? IssueDate { get; set; } // Issue_Date
        public string IssueBy { get; set; } // Issue_By (length: 50)
        public string ReceiveBy { get; set; } // Receive_By (length: 50)
        public int? PckIssue { get; set; } // PCK_Issue
        public double Quantity { get; set; } // Quantity
        public int RmId { get; set; } // RM_ID
        public string Unit { get; set; } // Unit (length: 50)
        public int? Rid { get; set; } // RID
        public int? Sid { get; set; } // SID
        public string PckNo { get; set; } // PCK_No (length: 50)
        public string RackNo { get; set; } // RackNo (length: 200)
        public string RmHead { get; set; } // RM Head (length: 50)
        public string Code { get; set; } // Code (length: 50)
        public int BelongTo { get; set; } // BelongTo
        public string FullName { get; set; } // Full_Name
        public double? Side { get; set; } // Side
        public string PName { get; set; } // P_Name

        public ViewIssuePackingMModify()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
