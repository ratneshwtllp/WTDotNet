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

    // View_Total_Weight
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewTotalWeight
    {
        public long PckId { get; set; } // PCK_Id
        public long OrderId { get; set; } // Order_ID
        public int PackAssNo { get; set; } // Pack_Ass_No
        public string Diemension { get; set; } // Diemension (length: 50)
        public int NoofOuter { get; set; } // Noof_Outer
        public int Pcs { get; set; } // Pcs
        public double GrossTot { get; set; } // Gross_tot
        public double NetTot { get; set; } // Net_tot
        public long InsId { get; set; } // Ins_ID

        public ViewTotalWeight()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
