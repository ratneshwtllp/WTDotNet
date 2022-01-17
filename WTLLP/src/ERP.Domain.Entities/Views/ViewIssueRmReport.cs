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

    // View_IssueRMReport
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewIssueRmReport
    {
        public long IssueId { get; set; } // Issue_ID
        public long IssueSNo { get; set; } // Issue_SNo
        public string IssueNo { get; set; } // Issue_No (length: 50)
        public long? PlanId { get; set; } // Plan_Id
        public System.DateTime? IssueDate { get; set; } // Issue_Date
        public System.DateTime PlanDate { get; set; } // Plan_Date
        public string Name { get; set; } // Name
        public int SupervisorId { get; set; } // Supervisor_Id
        public string ReceiveBy { get; set; } // Receive_By (length: 50)
        public int? PckIssue { get; set; } // PCK_Issue

        public ViewIssueRmReport()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>