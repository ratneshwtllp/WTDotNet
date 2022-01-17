using System;

namespace ERP.Domain.Entities
{
    public class ViewSaleOrderChild
    {
        public long RowID { get; set; }
        public long OrderMasterID { get; set; }
        public long OrderChildID { get; set; }
        public long FitemId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public long ProductSubCategoryID { get; set; }
        public string ProductSubCategoryName { get; set; }
        public long ProductCategoryID { get; set; }
        public int Qty { get; set; }
        public int Size { get; set; }               //sizeid
        public string ProductSizeName { get; set; }

        public double Price { get; set; }
        public double  Amount { get; set; }
        public string CLName { get; set; }

        public string PartyCode { get; set; }
        public string Barcode { get; set; }
        public string Logo { get; set; }
         
        public string OrderNo { get; set; }
        public long BuyerId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Remark { get; set; }
        public int TotalQty { get; set; }
        public double TotalAmount { get; set; }
        public int CancelStatus { get; set; }
        public int IssuedById { get; set; }
        public string RefrenceNo { get; set; }
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; }
        public string SizeBarcode { get; set; }

    }
}
