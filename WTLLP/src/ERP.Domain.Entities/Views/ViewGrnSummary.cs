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

    // The table 'View_GRNSummary' is not usable by entity framework because it
    // does not have a primary key. It is listed here for completeness.
    // View_GRNSummary
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewGrnSummary
    {
        public long? RItemId { get; set; } // RItem_Id
        public string Gipno { get; set; } // GIPNO (length: 20)
        public System.DateTime? GipDate { get; set; } // GIPDate
        public string Challanno { get; set; } // Challanno (length: 50)
        public string Billno { get; set; } // Billno (length: 50)
        public string Pono { get; set; } // PONO (length: 50)
        public System.DateTime? PoDate { get; set; } // PODate
        public double? Qty { get; set; } // Qty
        public string PName { get; set; } // P_Name
        public int? Verify { get; set; } // Verify
        public string ItemCode { get; set; } // Item_Code
        public string CustOrderNo { get; set; } // Cust_Order_No (length: 50)
        public long? Gipid { get; set; } // GIPID
        public long? TableId { get; set; } // Table_ID
        public long? OrderId { get; set; } // Order_Id

        public ViewGrnSummary()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>