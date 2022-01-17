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

    // View_RackInInpass
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewRackInInpass
    {
        public long IssueId { get; set; } // IssueID
        public System.DateTime IrmDate { get; set; } // IRMDate
        public long SampleId { get; set; } // SampleId
        public int IssueTo { get; set; } // IssueTo
        public int? ForSampleForChange { get; set; } // ForSample_ForChange
        public long? IssueSid { get; set; } // IssueSID
        public string IssueNo { get; set; } // IssueNo (length: 50)
        public int? Rid { get; set; } // RID
        public int? Sid { get; set; } // SID
        public int? Side { get; set; } // Side
        public string Comments { get; set; } // Comments (length: 500)
        public string PName { get; set; } // P_Name
        public long? Rmid { get; set; } // RMID
        public string OutPassNo { get; set; } // OutPass_No (length: 50)
        public string InPassNo { get; set; } // INPass_No (length: 50)
        public long? InPassId { get; set; } // INPass_ID
        public long InPassTableId { get; set; } // INPassTable_ID

        public ViewRackInInpass()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>