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

    // View_JWReceiveReport
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewJwReceiveReport
    {
        public long JwReceiveId { get; set; } // JWReceiveId
        public System.DateTime? ReceiveDate { get; set; } // ReceiveDate
        public long? PlanId { get; set; } // PlanId
        public double? TodayRecQty { get; set; } // TodayRecQty
        public string Coments { get; set; } // Coments (length: 500)
        public System.DateTime PlanDate { get; set; } // Plan_Date
        public string CustOrderNo { get; set; } // Cust_Order_No (length: 50)
        public string OrderNo { get; set; } // Order_No (length: 50)
        public string ConSupName { get; set; } // Con_Sup_Name
        public long OrderId { get; set; } // Order_Id
        public string Name { get; set; } // Name (length: 50)
        public string ItemCode { get; set; } // ItemCode (length: 50)

        public ViewJwReceiveReport()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
