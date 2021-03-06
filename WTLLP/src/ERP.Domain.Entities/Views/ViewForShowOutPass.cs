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

    // View_ForShowOutPass
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewForShowOutPass
    {
        public long? Qty { get; set; } // Qty
        public string OutPassNo { get; set; } // OutPass_No (length: 50)
        public System.DateTime? OutPassDate { get; set; } // OutPass_Date
        public string Comments { get; set; } // Comments
        public string OutPassBy { get; set; } // OutPass_By (length: 50)
        public string Name { get; set; } // Name (length: 50)
        public string Code { get; set; } // Code (length: 50)
        public string FullName { get; set; } // Full_Name
        public string PUnit { get; set; } // PUnit (length: 50)
        public long? RItemId { get; set; } // RItem_Id
        public long TableId { get; set; } // Table_ID
        public int? RmOrCg { get; set; } // RM_or_CG
        public string Purpose { get; set; } // Purpose

        public ViewForShowOutPass()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
