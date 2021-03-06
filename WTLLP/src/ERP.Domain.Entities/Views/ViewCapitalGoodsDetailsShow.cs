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

    // View_CapitalGoodsDetailsShow
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewCapitalGoodsDetailsShow
    {
        public int CapId { get; set; } // CapID
        public string CapCode { get; set; } // CapCode (length: 50)
        public string Cat3CapName { get; set; } // Cat3CapName (length: 50)
        public string Remarks { get; set; } // Remarks
        public int BelongsTo { get; set; } // Belongs_To
        public int ItOrCat3 { get; set; } // It_or_Cat3
        public string Cat2CapName { get; set; } // Cat2CapName (length: 50)
        public int ItOrCat2 { get; set; } // It_or_Cat2
        public int ItOrCat1 { get; set; } // It_or_Cat1
        public string Cat1CapName { get; set; } // Cat1CapName (length: 50)
        public int? OpeningStock { get; set; } // OpeningStock
        public string Unit { get; set; } // Unit (length: 50)
        public double? Price { get; set; } // Price
        public string CompanyName { get; set; } // CompanyName

        public ViewCapitalGoodsDetailsShow()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
