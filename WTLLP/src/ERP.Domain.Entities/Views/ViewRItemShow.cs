using System;

namespace ERP.Domain.Entities
{
    // View_RItemShow
    public class ViewRItemShow
    {
        public long CategoryID { get; set; } // RItem_Id
        public string CategoryName { get; set; } // Name (length: 50)
        public long SubCategoryID { get; set; } // RItem_Id
        public string SubCategoryName { get; set; } // Name (length: 50)
        public long RitemId { get; set; } // RItem_Id
        public string Name { get; set; } // Name (length: 50)
        public string Code { get; set; } // Code (length: 50)
        public string Title { get; set; } // Title
        public int CostUnit { get; set; } // cost unit 
        public string CostUnitName { get; set; } // cost unit
        public string CostUnitSName { get; set; } // cost unit

        //public long RItemId { get; set; } // RItem_Id
        //public string Name { get; set; } // Name (length: 50)
        //public string Code { get; set; } // Code (length: 50)
        //public int BelongTo { get; set; } // BelongTo
        public int MasterBelongTo { get; set; } // MasterBelongTo
        //public int ItOrCat { get; set; } // It_OR_Cat
        public string Finish { get; set; } // Finish (length: 50)
        public string Thickness { get; set; } // Thickness (length: 50)
        public string Colour { get; set; } // Colour (length: 50)
        //public string Shape { get; set; } // Shape (length: 50)
        //public string NickleFree { get; set; } // NickleFree (length: 50)
        public string SizeName { get; set; } // SizeName (length: 50)
        //public string WireSize { get; set; } // WireSize (length: 50)
        //public string Design { get; set; } // Design (length: 50)
        //public string Stone { get; set; } // Stone (length: 50)
        //public string Dim1 { get; set; } // Dim1 (length: 50)
        //public string Dim2 { get; set; } // Dim2 (length: 50)
        //public string Diameter { get; set; } // Diameter (length: 50)
        //public string PickThickness { get; set; } // PickThickness (length: 50)
        public int PUnit { get; set; } // PUnit (length: 50)
        public string PUnitName { get; set; } // cost unit
        public string PUnitSName { get; set; } // cost unit
        //public decimal? PucrhasePrice { get; set; } // PucrhasePrice
        //public string CostUnit { get; set; } // CostUnit (length: 50)
        public double CostPrice { get; set; } // CostPrice
        public double ConversionFactor { get; set; } // ConversionFactor
        //public double? OpStock { get; set; } // Op_Stock
        //public double? MinStock { get; set; } // Min_Stock
        //public double? MaxStock { get; set; } // Max_Stock
        //public double? Was { get; set; } // WAS
        //public double? BomWasPercent { get; set; } // BOM_WAS_Percent
        //public string PanningWay { get; set; } // PanningWay (length: 50)
        //public string Stucture { get; set; } // Stucture (length: 50)
        //public string Washable { get; set; } // Washable (length: 50)
        //public string AverageArea { get; set; } // AverageArea (length: 50)
        //public string Selection { get; set; } // Selection (length: 50)
        //public string StrenthTest { get; set; } // StrenthTest (length: 50)
        //public string Base { get; set; } // Base (length: 50)
        //public string DyedSpe { get; set; } // DyedSpe (length: 50)
        //public string EuNorms { get; set; } // EuNorms (length: 50)
        //public int? MinDeliveryDays { get; set; } // MinDeliveryDays
        //public string Dnsity { get; set; } // Dnsity (length: 50)
        //public string SessionYear { get; set; } // Session_year (length: 20)
        public int UserId { get; set; } // UserID
        public string ColorFast { get; set; } // ColorFast (length: 6)
        public string Description { get; set; } // Description
        //public string Cloth { get; set; } // Cloth (length: 50)
        //public string Metal { get; set; } // Metal (length: 50)
        //public string Quality { get; set; } // Quality (length: 50)
        //public string Adhesive { get; set; } // Adhesive (length: 50)
        //public string Cartoon { get; set; } // Cartoon (length: 50)
        public string Full_Name { get; set; } // Full_Name
        //public string PurchaseType { get; set; } // Purchase_Type (length: 50)
        //public string ActualPUnit { get; set; } // ActualPUnit (length: 50)
        //public decimal? ActualPurPrice { get; set; } // ActualPurPrice
        //public string ActualCostUnit { get; set; } // ActualCostUnit (length: 50)
        //public decimal? ActualCostPrice { get; set; } // ActualCostPrice
        //public double? ActualConversionFactor { get; set; } // ActualConversionFactor
        //public long? RmCode { get; set; } // RM_Code
        public string SuppCode { get; set; }
        //public long? MonthYear { get; set; } // Month_Year
        //public string Title { get; set; } // Title
        //public double? MaxWastage { get; set; } // Max_Wastage
        //public string CName { get; set; } // CName (length: 50)
        //public string AddressOffice { get; set; } // Address_office
        //public string Rm { get; set; } // RM
        //public string Subcategory { get; set; } // Subcategory (length: 50)
        //public string Category { get; set; } // Category (length: 50)
        //public long MainCatId { get; set; } // main_cat_id
        public DateTime CreatedDate { get; set; }
        public int RMBrandId { get; set; }
        public string RMBrandName { get; set; }
        public string RMBrandCode { get; set; }
        public string RMBrandDesc { get; set; }

        public int HSNId { get; set; }
        public string HSNCode { get; set; }
        public int TaxId { get; set; }

        public string TaxName { get; set; }
        public double TaxPer { get; set; }
        public string TaxFullName { get; set; }
        public string UserName { get; set; }
        public int UserTypeId { get; set; }

        public DateTime RMUpdateOn { get; set; }
        public int RMUpdateUser { get; set; }
        public int RMReviewStatus { get; set; }
        public DateTime RMReviewOn { get; set; }
        public int RMReviewUser { get; set; }
        public string RMUpdateUserName { get; set; }
        public string RMReviewUserName { get; set; }

        public double RMSuppMaxPrice { get; set; }
        
    }

}
