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

    // View_HangtagDetails
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.24.0.0")]
    public partial class ViewHangtagDetail
    {
        public int Hid { get; set; } // HID
        public string HtName { get; set; } // HTName (length: 50)
        public string Description { get; set; } // Description (length: 100)
        public int? BuyerId { get; set; } // Buyer_Id
        public string PName { get; set; } // P_Name
        public string Address { get; set; } // Address
        public string City { get; set; } // City

        public ViewHangtagDetail()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
