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

    // View_IssueRM
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewIssueRm
    {
        public string IssueNo { get; set; } // Issue_No (length: 50)
        public System.DateTime? IssueDate { get; set; } // Issue_Date
        public string IssueBy { get; set; } // Issue_By (length: 50)
        public string ReceiveBy { get; set; } // Receive_By (length: 50)
        public int RmId { get; set; } // RM_ID
        public string FullName { get; set; } // Full_Name
        public double Quantity { get; set; } // Quantity
        public int BelongTo { get; set; } // BelongTo
        public string Unit { get; set; } // Unit (length: 50)
        public int? ExtraIssue { get; set; } // ExtraIssue
        public double ReqQty { get; set; } // Req_Qty
        public string Code { get; set; } // Code (length: 50)
        public string Comments { get; set; } // Comments
        public long IssueSNo { get; set; } // Issue_SNo
        public string RmHead { get; set; } // RM Head (length: 50)
        public long IssueId { get; set; } // Issue_ID
        public System.DateTime PlanDate { get; set; } // Plan_Date
        public long PlanId { get; set; } // Plan_Id
        public long OrderId { get; set; } // Order_Id
        public string Expr1 { get; set; } // Expr1
        public int PlanFor { get; set; } // Plan_For
        public string PlanBy { get; set; } // Plan_By (length: 50)
        public string Color { get; set; } // Color (length: 50)
        public string ItemSize { get; set; } // Item_Size (length: 50)
        public int PlanQty { get; set; } // Plan_Qty
        public string ItemCode { get; set; } // ItemCode (length: 500)
        public int SupervisorId { get; set; } // Supervisor_Id
        public string ConSupName { get; set; } // Con_Sup_Name
        public int? Sid { get; set; } // SID
        public double? Side { get; set; } // Side
        public string PName { get; set; } // P_Name
        public long Rid { get; set; } // RID
        public string RackNo { get; set; } // RackNo (length: 200)
        public string UName { get; set; } // UName (length: 50)
        public string Aboutuser { get; set; } // Aboutuser (length: 300)
        public string CustOrderNo { get; set; } // Cust_Order_No (length: 50)
        public long FItemId { get; set; } // FItem_Id

        public ViewIssueRm()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
