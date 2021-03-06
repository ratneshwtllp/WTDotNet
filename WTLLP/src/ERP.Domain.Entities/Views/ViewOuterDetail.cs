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

    // View_OuterDetails
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewOuterDetail
    {
        public int OrderId { get; set; } // Order_Id
        public long InsId { get; set; } // Ins_ID
        public long PackingNo { get; set; } // Packing_No
        public int NoOfInner { get; set; } // no_of_inner
        public int InnerInCarton { get; set; } // inner_in_carton
        public int NoofOuter { get; set; } // Noof_outer
        public string InDim { get; set; } // In_dim (length: 50)
        public string CDim { get; set; } // C_dim (length: 50)
        public string CustOrderNo { get; set; } // Cust_Order_No (length: 50)

        public ViewOuterDetail()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
