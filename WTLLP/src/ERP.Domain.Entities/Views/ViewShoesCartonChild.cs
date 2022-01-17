using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewShoesCartonChild
    {
        //ShoesCartonMaster
        public long ShoesCartonMasterId { get; set; }
        public long BuyerId { get; set; }
        public long OrderId { get; set; }
        public DateTime ShoesCartonDate { get; set; }
        public long CartonNo { get; set; }
        public string BuyerCartonNo { get; set; }
        public string ExporterCode { get; set; }
        public int CartonId { get; set; }
        public string Dimension { get; set; }
        public int TotalQty { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
         
        //ShoesCartonChild
        public long ShoesCartonChildId { get; set; }
        //public long ShoesCartonMasterId { get; set; }
        public long OrderChildId { get; set; }
        public long FitemId { get; set; }
        public int SizeId { get; set; }
        public string Barcode { get; set; }
        public int Qty { get; set; }
        public int SNo { get; set; }
         
        //Buyer 
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; }

        //productsize
        public string ProductSizeName { get; set; } 

        //productdetails
        public string Name { get; set; } 
        public string Code { get; set; }
        public string Unit { get; set; }
        //public string PartyCode { get; set; }
        //public string Description { get; set; }
        public int Color { get; set; }
         
        //saleordermaster
        public string OrderNo { get; set; }
          
        //color
        public string CLName { get; set; }

        ////unit
        //public string UName { get; set; }
        //public string USName { get; set; }



    }
}
