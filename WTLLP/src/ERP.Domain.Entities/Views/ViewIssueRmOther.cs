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

    // View_Issue_RM_Other
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewIssueRmOther
    {
        public long OtherId { get; set; } // Other_ID
        public long OtherSNo { get; set; } // Other_SNo
        public string OtherNo { get; set; } // Other_No (length: 50)
        public System.DateTime? OtherDate { get; set; } // Other_Date
        public string ReceiveBy { get; set; } // Receive_By (length: 50)
        public long ItemId { get; set; } // Item_Id
        public string CustOrderNo { get; set; } // Cust_Order_No (length: 50)
        public string PName { get; set; } // P_Name
        public string RackNo { get; set; } // RackNo (length: 200)
        public string Remark { get; set; } // Remark
        public int SupervisorId { get; set; } // Supervisor_Id
        public string ItemCode { get; set; } // ItemCode (length: 500)
        public long? PlanId { get; set; } // Plan_Id
        public System.DateTime PlanDate { get; set; } // Plan_Date
        public string Code { get; set; } // Code (length: 50)
        public string RmHead { get; set; } // RM Head (length: 50)
        public string FullName { get; set; } // Full_Name
        public double Quantity { get; set; } // Quantity
        public double? Side { get; set; } // Side
        public string Unit { get; set; } // Unit (length: 50)
        public int RmId { get; set; } // RM_ID
        public string Name { get; set; } // Name
        public string CName { get; set; } // CName (length: 50)
        public string AddressWork { get; set; } // Address_work
        public string AddressOffice { get; set; } // Address_office

        public ViewIssueRmOther()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
