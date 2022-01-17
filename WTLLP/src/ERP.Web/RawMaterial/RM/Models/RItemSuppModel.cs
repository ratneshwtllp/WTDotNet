using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.RM.Models
{
    public partial class RItemSuppModel
    {
        public String SupplierName { get; set; }
        public String SupplierCode { get; set; }
        public long SupplierId { get; set; }

        public string RMCategory { get; set; }
        public string RMSubCategory { get; set; }
        public string Full_Name { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long RItem_Id { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public double Rate { get; set; }
        public int Min_Days { get; set; }
        public DateTime EntryDate { get; set; }
        public bool Selected { get; set; }
        public double CostPrice { get; set; }
        public int PUnit { get; set; }
        public string PUnitSName { get; set; }
        public int CostUnit { get; set; }

        public double Price { get; set; }
        //public int MinDays { get; set; }
        public long RItemSuppID { get; set; }
        public string SupplierRMCode { get; set; }

        public string CurrencyName { get; set; }
        public int SuppUnit { get; set; }
        public string SuppUnitName { get; set; }
        public string SuppUnitSName { get; set; }
         
        //RMForProduct
        public string ProductCategory { get; set; } 
        public string ProductSubCategory { get; set; } 
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string PhotoPath { get; set; }

        //RMMinimumLevel
        public string MiniLevelRMName { get; set; }
        public string MiniLevelCode { get; set; }
        public double MiniLevelStock { get; set; }
        public string MiniLevelUnit { get; set; }
        public double MiniLevel { get; set; }
        //public string MiniLevelTitle { get; set; }

        public long FItem_Id { set; get; }
        public long CostingID { get; set; }
        public int SizeId { get; set; }
        public string ProductSizeName { get; set; }


        //RM Supplier Price History
        public double OldPrice { get; set; }
        public double NewPrice { get; set; }
        public double ConversionFactor { set; get; }
        public string CUnitSName { get; set; }
        public double PCSWeight { set; get; }
    }
}
