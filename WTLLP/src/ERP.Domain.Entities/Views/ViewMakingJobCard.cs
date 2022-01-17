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

    // View_MakingJobCard
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewMakingJobCard
    {
        public string ItemCode { get; set; } // ItemCode (length: 500)
        public int PlanQty { get; set; } // Plan_Qty
        public long JbcId { get; set; } // JBC_ID
        public string JbcNo { get; set; } // JBC_No (length: 50)
        public System.DateTime JbcDate { get; set; } // JBC_Date
        public int JbcQty { get; set; } // JBC_Qty
        public long PlanId { get; set; } // Plan_Id
        public long OrderId { get; set; } // Order_Id
        public long WorkerId { get; set; } // Worker_ID
        public string ConSupName { get; set; } // Con_Sup_Name
        public string Name { get; set; } // Name
        public decimal Rate { get; set; } // Rate
        public long JbcSNo { get; set; } // JBC_SNo
        public decimal Amount { get; set; } // Amount
        public string OrderNo { get; set; } // Order_No (length: 50)

        public ViewMakingJobCard()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>