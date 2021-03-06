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

    // View_FinishGoodReceive
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewFinishGoodReceive
    {
        public long PlanId { get; set; } // PlanId
        public string ReceiveBy { get; set; } // ReceiveBy (length: 50)
        public string Remark { get; set; } // Remark
        public System.DateTime FrDate { get; set; } // FRDate
        public System.DateTime PlanDate { get; set; } // Plan_Date
        public long OrderId { get; set; } // Order_Id
        public string CustOrderNo { get; set; } // Cust_Order_No (length: 50)
        public string ConSupName { get; set; } // Con_Sup_Name
        public string Name { get; set; } // Name (length: 50)
        public string ItemCode { get; set; } // ItemCode (length: 50)

        public ViewFinishGoodReceive()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
