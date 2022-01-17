using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewShoesPacking
    {
        //packingchild
        public long PackingChildID { get; set; }
        public long PackingID { get; set; }
        public long OrderChildID { get; set; }
        public int CartonNo { get; set; }
        public int CartonID { get; set; }
        public int PackingQty { get; set; }
        public int SNo { get; set; } 
        public string Barcode { get; set; }
        public long FitemId { get; set; }

        //packingmaster 
        public long BuyerID { get; set; }
        public string PackingNo { get; set; } 
        public long PackingSerial { get; set; } 
        public DateTime PackingDate { get; set; }
        public int TotalPackingQty { get; set; }
        //public int IsWeigh { get; set; }

        //Buyer 
        public string BuyerName { get; set; }
        public int CurrencyID { get; set; }

        //currency
        public string CName { get; set; }
        public string CSName { get; set; }

        //productdetails
        public string Name { get; set; } 
        public string Code { get; set; }
        public string Unit { get; set; }
        public string PartyCode { get; set; }
        public string Description { get; set; }
        public int Color { get; set; }
        public double Price { get; set; }
          
        //saleordermaster
        public string OrderNo { get; set; }
          
        //color
        public string CLName { get; set; }

        public int ProductSizeId { get; set; }
        public string SizeBarcode { get; set; }
        public long ProductMultipleSizeId { get; set; }
        public string ProductSizeName { get; set; }


    }
}
