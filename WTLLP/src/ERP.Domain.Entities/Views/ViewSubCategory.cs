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

    // View_SubCategory
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewSubCategory
    {
        public long RItemId { get; set; } // RItem_Id
        public string Code { get; set; } // Code (length: 50)
        public string Name { get; set; } // Name (length: 50)
        public int BelongTo { get; set; } // BelongTo
        public long SubCatId { get; set; } // sub_cat_id
        public string SubCatName { get; set; } // sub_cat_name (length: 50)
        public string SubCatCode { get; set; } // sub_cat_code (length: 50)
        public int Expr4 { get; set; } // Expr4
        public string Description { get; set; } // Description

        public ViewSubCategory()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
