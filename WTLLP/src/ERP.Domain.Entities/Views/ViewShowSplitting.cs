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

    // View_Show_Splitting
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewShowSplitting
    {
        public System.DateTime SplittingDate { get; set; } // Splitting_Date
        public long PlanId { get; set; } // Plan_Id
        public long? QcQty { get; set; } // Qc_Qty
        public string Spec { get; set; } // Spec (length: 50)
        public long? SplittingQty { get; set; } // Splitting_Qty
        public string Comments { get; set; } // Comments (length: 150)
        public long Expr1 { get; set; } // Expr1

        public ViewShowSplitting()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
