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

    // View_ROP_CP
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewRopCp
    {
        public long RopTableId { get; set; } // ROP_Table_ID
        public long RopId { get; set; } // ROP_ID
        public string RopNo { get; set; } // ROP_No (length: 50)
        public System.DateTime RopDate { get; set; } // ROP_Date
        public string Comments { get; set; } // Comments
        public string OutPassBy { get; set; } // OutPass_By (length: 50)
        public string SessionYear { get; set; } // Session_Year (length: 50)
        public int UserId { get; set; } // User_Id
        public int CapId { get; set; } // CapID
        public string CapCode { get; set; } // CapCode (length: 50)
        public string CapName { get; set; } // CapName (length: 50)
        public long Qty { get; set; } // Qty
        public string Purpose { get; set; } // Purpose
        public string SubCategoryCode { get; set; } // SubCategory_Code (length: 50)
        public string SubCategoryName { get; set; } // SubCategory_Name (length: 50)

        public ViewRopCp()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
