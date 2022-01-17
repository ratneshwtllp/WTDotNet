using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    // ViewProduct
    public class ViewShowCosting
    {
        public long CostingID { get; set; }
        public long FitemID { get; set; }
        public double RMTotal { get; set; }
        public double ElementTotal { get; set; }
        public double Forwording { get; set; }
        public double Freight { get; set; }
        public double Packing { get; set; }
        public double Labour { get; set; }
        public double GrandTotal { get; set; }
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

        public double ManDays { get; set; }
        public string Sessionyear { get; set; }
        public int UserID { get; set; }
        public string Remark { get; set; }
        public int FormType { get; set; }
        public DateTime CreationDate { get; set; }
        public int IdentifiedBy { get; set; }
        public double BomRMTotal { get; set; }
        public double BOMGrandTotal { get; set; }
        public double BOMOHPerAmt { get; set; }
        public double BomOHAmt { get; set; }
        public double BOMProfitPerAmt { get; set; }
        public double BomProfitAmt { get; set; }
        public double BOMComPerAmt { get; set; }
        public double BomComAmt { get; set; }
        public double BomCostingPrice { get; set; }
        public double BomAfterLabour { get; set; }
        public double BomAfterTax { get; set; }

        public double HighPer { get; set; }
        public double MedPer { get; set; }
        public double Lowper { get; set; }
        public double Increaseper { get; set; }
        public string FinishInHouse { get; set; }
        public string Coments { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Code { get; set; }      
        public string ProductSubCategoryName { get; set; }
        public long ProductCategoryID { get; set; }
        public string ProductCategoryName { get; set; }
        public long ProductSubCategoryID { get; set; }
        public string PhotoPath { get; set; }
        public int SizeId { get; set; }
        public int CLID { get; set; }
        public string CLName { get; set; }
        public string ProductSizeName { get; set; }
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; }
        public long BuyerId { get; set; }
        public DateTime UpdationDate { get; set; }

        public int MatReviewedStatus { get; set; }
        public DateTime MatReviewedDate { get; set; }
        public int MatReviewedUserId { get; set; }

        public double PriceInUSD { get; set; }
        public double USDConversionRate { set; get; }

        public double CurrentUSDRate { set; get; }
        public double CurrentUSDValue { set; get; }
    }
}
