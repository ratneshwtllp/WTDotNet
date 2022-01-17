using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewShoesPackingChild
    {
        //PackingMaster
        public long PackingID { get; set; }
        public long BuyerID { get; set; }
        public string PackingNo { get; set; }
        public long PackingSerial { get; set; }
        public DateTime PackingDate { get; set; }
        public int TotalPackingQty { get; set; }
        //public int IsWeigh { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserID { get; set; }
        public int TotalCarton { get; set; }

        //PackingChild
        public long PackingChildID { get; set; }
        //public long PackingID { get; set; }
        public long OrderChildID { get; set; }
        public int CartonNo { get; set; }
        public int CartonID { get; set; }
        public int PackingQty { get; set; }
        public int SNo { get; set; }
        public string Barcode { get; set; }
        public long FitemId { get; set; }
        public long ShoesCartonMasterId { get; set; }
        public int SizeId { get; set; }

        //Buyer 
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; }

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

        //unit
        //public string UName { get; set; }
        //public string USName { get; set; }


        //productsize
        public string ProductSizeName { get; set; }
         
    }
}
