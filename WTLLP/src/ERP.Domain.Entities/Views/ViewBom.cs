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

    // View_BOM
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewBom
    {
        public long CostingId { get; set; } // Costing_Id
        public string Item { get; set; } // Item (length: 100)
        public long? RmId { get; set; } // RM_id
        public double? Reqd { get; set; } // REQD
        public string Pcs { get; set; } // PCS (length: 50)
        public double? ActualQty { get; set; } // Actual_Qty
        public double? BomWas { get; set; } // BOMWas
        public string Name { get; set; } // Name (length: 50)
        public long SubCatId { get; set; } // Sub_CatID
        public string Code { get; set; } // Code (length: 50)
        public double? ConversionFactor { get; set; } // ConversionFactor
        public double? MaxWastage { get; set; } // Max_Wastage
        public string PUnit { get; set; } // PUnit (length: 50)
        public long MainCatId { get; set; } // Main_Cat_ID
        public string Category { get; set; } // Category (length: 50)

        public ViewBom()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
