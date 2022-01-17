using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using ERP.Web.Areas.RMProperty.Models;

namespace ERP.Web.Areas.RM.Models
{
    public class RawMaterialModel
    {
        [Display(Name = "Raw Material")]
        public long RItem_ID { set; get; }
        public string Name { set; get; }
        [Display(Name = "Raw Material Code")]
        public string Code { set; get; }
        public int BelongTo { set; get; }
        public int MasterBelongtTo { set; get; }
        public int It_OR_Cat { set; get; }
        public string Finish { set; get; }
        public string Thickness { set; get; }
        public string Colour { set; get; }
        [Display(Name = "Shape/State")]
        public string Shape { set; get; }
        [Display(Name = "Nickle Free")]
        public string NickleFree { set; get; }
        [Display(Name = "Size")]
        public string SizeName { set; get; }
        [Display(Name = "Wire Size")]
        public string WireSize { set; get; }
        public string Design { set; get; }
        public string Stone { set; get; }
        public string Dim1 { set; get; }
        public string Dim2 { set; get; }
        public string Diameter { set; get; }
        public string PickThickness { set; get; }

        [Display(Name = "Purchase Unit")]
        public int PUnit { set; get; }
        public string PUnitSName { set; get; }

        [Display(Name = "Purchase Price")]
        public double PucrhasePrice { set; get; }

        [Display(Name = "Factory Unit")]
        public int CostUnit { set; get; }
        public string CostUnitSName { set; get; }
        
        [Display(Name = "Costing Price")]
        public double CostPrice { set; get; }
        [Display(Name = "Conversion Factor")]
        public double ConversionFactor { set; get; }
        public int Op_Stock { set; get; }
        [Display(Name = "Minimum Level")]
        public double  Min_Stock { set; get; }
        [Display(Name = "Maximum Level")]
        public double Max_Stock { set; get; }
        [Display(Name = "Costing Wast. %")]
        public double WAS { set; get; }
        [Display(Name = "BOM Wast. %")]
        public double BOM_WAS_Percent { set; get; }
        [Display(Name = "Tanning")]
        public string PanningWay { set; get; }
        public string Stucture { set; get; }
        public string Washable { set; get; }
        [Display(Name = "Average Area")]
        public string AverageArea { set; get; }
        public string Selection { set; get; }
        [Display(Name = "Strenth Test")]
        public string StrenthTest { set; get; }
        public string Base { set; get; }
        [Display(Name = "Dye Shape")]
        public string DyedSpe { set; get; }
        public string EuNorms { set; get; }
        [Display(Name = "Min Delivery Days")]
        public int MinDeliveryDays { set; get; }
        [Display(Name = "Density")]
        public string Dnsity { set; get; }
        public string Session_year { set; get; }
        public int UserID { set; get; }
        [Display(Name = "Color Fasting")]
        public string ColorFast { set; get; }
        [Display(Name = "RM Description")]
        public string Description { set; get; }
        public string Cloth { set; get; }
        public string Metal { set; get; }
        [Display(Name = "Quality/Grade")]
        public string Quality { set; get; }
        public string Adhesive { set; get; }
        [Display(Name = "Carton")]
        public string Cartoon { set; get; }
        public string Full_Name { set; get; }
        public string Purchase_Type { set; get; }
        public string ActualPUnit { set; get; }
        [Display(Name = "PCS Weight (grm)")]
        public double ActualPurPrice { set; get; }
        public string ActualCostUnit { set; get; }
        public double ActualCostPrice { set; get; }
        public double ActualConversionFactor { set; get; }
        public string RM_Code { set; get; }
        [Display(Name = "Supplier Description")]
        public string SuppCode { set; get; }
        public int Month_Year { set; get; }
        public string Title { set; get; }
        [Display(Name = "Maximum Wastage")]
        public double Max_Wastage { set; get; }
        public string Remarks { set; get; }

        [Display(Name = "RM Update On")]
        public DateTime RMUpdateOn { get; set; }
        public bool ChkRMUpdateOn { get; set; }
        public int ChkRMUpdateOnInt { get; set; }
        public int RMUpdateUser { get; set; }

        public int RMReviewStatus { get; set; }
        [Display(Name = "RM Review On")]
        public DateTime RMReviewOn { get; set; }
        public bool ChkRMReviewOn { get; set; }
        public int ChkRMReviewOnInt { get; set; }
        public int RMReviewUser { get; set; }

        [Display(Name = "RM Category")]
        public int Cat_Id { set; get; }

        [Display(Name = "RM Sub Category")]
        public int Subcat_Id { set; get; }
        [Display(Name = "Width")]
        public string Width { get; set; }
        [Display(Name = "GSM")]
        public int GSM { get; set; }
        //RItem_Id, Name, Code, BeintTo, MasterBeintTo, It_OR_Cat, Finish, Thickness, Colour, Shape, NickleFree, SizeName, WireSize, Design,
        //Stone, Dim1, Dim2, Diameter, PickThickness, PUnit, PucrhasePrice, CostUnit, CostPrice, ConversionFactor, Op_Stock, Min_Stock, Max_Stock,
        //WAS, BOM_WAS_Percent, PanningWay, Stucture, Washable, AverageArea, Selection, StrenthTest, Base, DyedSpe, EuNorms, MinDeliveryDays, Dnsity,
        //Session_year, UserID, ColorFast, Description, Cloth, Metal, Quality, Adhesive, Cartoon, Full_Name, Purchase_Type, ActualPUnit, ActualPurPrice,
        //ActualCostUnit, ActualCostPrice, ActualConversionFactor, RM_Code, SuppCode, Month_Year, Title, Max_Wastage

        [Display(Name = "RM Category")]
        public string RMCategory { set; get; }

        [Display(Name = "RM Sub Category")]
        public string RMSubCategory { set; get; }

        public SelectList RMCategoryList { set; get; }
        public SelectList RMSubCategoryList { set; get; }
        public SelectList FinishList { set; get; }
        public SelectList ThicknessList { set; get; }
        public SelectList ColorList { set; get; }
        public SelectList ShapeList { set; get; }

        public SelectList NickleFreeList { set; get; }
        public SelectList SizeList { set; get; }
        public SelectList MetalList { set; get; }

        public SelectList CartoonList { set; get; }
        public SelectList TanningList { set; get; }
        public SelectList QualityList { set; get; }
        public SelectList StructureList { set; get; }
        public SelectList SelectionList { set; get; }
        public SelectList AdhesiveList { set; get; }
        public SelectList ClothList { set; get; }

        public SelectList WiresizeList { set; get; }
        public SelectList StoneList { set; get; }
        public SelectList PinThicknessList { set; get; }

        public SelectList PurchaseUnitList { set; get; }
        public SelectList CostUnitList { set; get; } 

        public SelectList RawMaterialList { set; get; }
        public SelectList QuickTestList { set; get; }

        public List<RItemSuppModel> RItemSuppModel { get; set; }

        public SelectList WidthList { get; set; }
        public SelectList GSMList { get; set; }

        public SelectList RMBrandList { get; set; }
        [Display(Name = "Brand")]
        public int RMBrandId { get; set; }
        public string RMBrandName { get; set; }
        public string RMBrandCode { get; set; }

        //RM Rate
        public string CurrencyName { get; set; }
        public SelectList SuppUnitList { get; set; }

        //Supplier
        [Display(Name = "Supplier")]
        public long SupplierId { get; set; }
        public SelectList SupplierList { set; get; }

        public int ColorId { get; set; }
        public DateTime CreatedDate { set; get; }

        [Display(Name = "HSN")]
        public int HSNId { get; set; }
        public SelectList HSNList { set; get; }

        //Search Date From
        public bool ChkCreateDateFrom { get; set; }
        public int ChkCreateDateFromInt { get; set; }
        [Display(Name = "Create From")]
        public DateTime CreateDateFrom { set; get; }
        //Search Date To
        public bool ChkCreateDateTo { get; set; }
        public int ChkCreateDateToInt { get; set; }
        [Display(Name = "Create To")]
        public DateTime CreateDateTo { set; get; }

        public List<RMFileModel> RMFileModel { get; set; }
        public string FileUploadMessage { get; set; }

        [Display(Name = "Document Type")]
        public int DocumentTypeId { set; get; }
        public SelectList DocumentTypeList { set; get; }

        public SelectList CertificateRequiredForPuchaseList { get; set; }
        [Display(Name = "Certificate Required For Purchase")]
        public int CertReqForPuchaseId { get; set; }
        public string CertReqForPuchase { get; set; }
        public int IsCertificateRequiredForPurchase { get; set; }

        [Display(Name = "Valid From")]
        public DateTime ValidFrom { get; set; }
        [Display(Name = "Valid To")]
        public DateTime ValidTo { get; set; }
        [Display(Name = "Alert In Days")]
        public int AlertInDays { get; set; }

        public int IsReveiew { get; set; }
        public DateTime ReviewDate { get; set; }
        public int ReviewStatus { get; set; }

        public List<RMPropertiesModel> RMPropertiesModel { get; set; }
        public List<RMPValueModel> RMPValueModel { get; set; }
        public long RMPropertiesValueID { get; set; }
        public SelectList RMPValueList { get; set; }
        public int SizeID { set; get; }
        public bool EditMode { get; set; }
        public SelectList RMCodeList { set; get; }
        public SelectList RMNameList { set; get; }

        public long UpdateRItemID { get; set; }

        public List<QuickTestModel> QuickTestModel { get; set; }

        [Display(Name = "Date From")]
        public DateTime DateFrom { set; get; }
        [Display(Name = "Date To")]
        public DateTime DateTo { set; get; }

        public double RMSuppMaxPrice { get; set; }
    }
}