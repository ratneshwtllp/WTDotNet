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

    // View_PIReport
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewPiReport
    {
        public long PiId { get; set; } // PI_ID
        public long PiSessionId { get; set; } // PI_Session_ID
        public string PiNo { get; set; } // PI_No (length: 50)
        public System.DateTime PiDate { get; set; } // PI_Date
        public string CustOrderNo { get; set; } // Cust_Order_No (length: 50)
        public System.DateTime Date { get; set; } // Date
        public long OrderId { get; set; } // Order_Id
        public int? Status { get; set; } // Status
        public System.DateTime? DeliveryDate { get; set; } // Delivery_Date
        public string CsName { get; set; } // CSName (length: 50)
        public int Qty { get; set; } // Qty
        public decimal Rate { get; set; } // Rate
        public decimal Amount { get; set; } // Amount
        public string ItemCode { get; set; } // ItemCode (length: 500)
        public string Brand { get; set; } // Brand (length: 50)
        public long ItemId { get; set; } // Item_Id

        public ViewPiReport()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
