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

    // View_RecvFromBuyerDetails
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewRecvFromBuyerDetail
    {
        public long ReciptsId { get; set; } // Recipts_ID
        public long? ReciptsNo1 { get; set; } // Recipts_No
        public string ReciptsNo { get; set; } // ReciptsNo (length: 50)
        public System.DateTime? ReciptsDate { get; set; } // Recipts_Date
        public int? BuyerId { get; set; } // Buyer_ID
        public int? MadiatorId { get; set; } // Madiator_ID
        public int? AgainstOrder { get; set; } // AgainstOrder
        public int? OrderId { get; set; } // Order_ID
        public string Remarks { get; set; } // Remarks
        public string AwbNo { get; set; } // Awb_No (length: 50)
        public string Type { get; set; } // Type (length: 50)
        public string Name { get; set; } // Name (length: 50)
        public int ItemId { get; set; } // Item_ID
        public int? Qty { get; set; } // Qty
        public string CustOrderNo { get; set; } // Cust_Order_No (length: 50)

        public ViewRecvFromBuyerDetail()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>