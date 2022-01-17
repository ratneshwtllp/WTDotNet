using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{ 
    public class ViewCostingRMPrint
    {
        public long CostingRMID { get; set; }
        public long CostingID { get; set; }
        public long RItemID { get; set; }
        public double RMQty { get; set; }
        public double CWAS { get; set; }
        public double CAfterWAS { get; set; }
        public double RMPrice { get; set; }
        public double RMCAmount { get; set; }
        public double BOMWAS { get; set; }
        public double BOMAfterWas { get; set; }
        public double RMBAmount { get; set; }
        public int SerialNo { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int CostUnit { get; set; }
        public string Full_Name { get; set; }
        public string Title { get; set; }
        public string CostUnitName { get; set; }
        public string CostUnitSName { get; set; }

        public long FitemID { get; set; }
        public string ProductCode { get; set; }
        public int SizeId { get; set; }
        public int CLID { get; set; }
        public string CLName { get; set; }
        public double Forwording { get; set; }
        public double Freight { get; set; }
        public double Packing { get; set; }
        public double Labour { get; set; }
        public double GrandTotal { get; set; }
        public double RMTotal { get; set; }
        public double OHPer { get; set; }
        public double OHPerAmt { get; set; }
        public double OHAmt { get; set; }
        public double ProfitPer { get; set; }
        public double ProfitPerAmt { get; set; }
        public double ProfitAmt { get; set; }
        public double ComPer { get; set; }
        public double ComPerAmt { get; set; }
        public double ComAmt { get; set; }
        public double CostingPrice { get; set; }
        public double SEAFreight { get; set; }

        public double AIRFreight { get; set; }
        public double CostingAfterLabour { get; set; }
        public double CostingAfterTaxes { get; set; }
        public string Coments { get; set; }
        public double ManDays { get; set; }
        public string CName { get; set; }
        public string AddressOffice { get; set; }
        public string Phone { get; set; }
        public string SizeName { get; set; }
        public double ElementTotal { get; set; }
        public int IsComponent{ get; set; }

        public long CostingRMComponentID { get; set; }
        public long CompID { get; set; }
        public string CompGroupName { get; set; }
        public string Comp_Name { get; set; }
        public int CompGroupId { get; set; }
        public string Remark { get; set; }  // rm component remark
        public string Description { get; set; }  //rm desc
        public string PRDWidth { get; set; } 
        public string Connstruction { get; set; }

        public string ProductDescription { get; set; }
        public string Heel { get; set; } 
        public string GenderName { get; set; }
        public string SoleName { get; set; }
        public string ProductName { get; set; }
        public string CompPhotoPath { get; set; }
        public double RMComQty { get; set; }
        public string PhotoPath { get; set; }
        public string ProductPhotoPath { get; set; }

        public double PriceInUSD { set; get; }
        public double USDConversionRate { set; get; }

        public string SuppUnit { set; get; }
        public int HSNId { set; get; }
        public string TaxFullName { set; get; }
        public double TaxPer { set; get; }
        public double RMSuppMaxPrice { set; get; }
        public double ConversionFactor { set; get; }
        public int DisplayOrder { set; get; }
    }
}
