using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewProductionPacking
    {
        //packingchild
        public string BuyerName { get; set; }
        public long OrderMasterID { get; set; }
        public long FitemId { get; set; }
        public string OrderNo { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string PartyCode { get; set; }
        public string Description { get; set; }
        public int Color { get; set; }
        public string CLName { get; set; }     
        public DateTime OrderDate { get; set; }
        public int Qty { get; set; }
        public string Barcode { get; set; }
        public string PackingNo { get; set; }
        public DateTime PackingDate { get; set; }
        public long PackingID { get; set; }    
        public string CName { get; set; }
        public string AddressOffice { get; set; }
        public string CortonString { get; set; }
        public int ProductSizeId { get; set; }
        public string ProductSizeName { get; set; }
        public int TotalPackingQty { get; set; }
        public int TotalCarton { get; set; }
    }
}
