using System;

namespace ERP.Domain.Entities
{
    public class ViewOrderItemsForPacking
    {
        //saleorderchild
        public long OrderMasterID { get; set; }
        public long OrderChildID { get; set; }
        public long FitemId { get; set; }
        public int Qty { get; set; }
        public int Size { get; set; }               //sizeid
        public double Price { get; set; }
        public double Amount { get; set; }

        //saleOrderMaster
        public string OrderNo { get; set; }
        public long BuyerId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Remark { get; set; }
        //public int TotalQty { get; set; }
        //public double TotalAmount { get; set; }
        public int CancelStatus { get; set; }
        public int IssuedById { get; set; }
        public string RefrenceNo { get; set; }

        //buyer
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; }
         
        //product sub category
        public long ProductSubCategoryID { get; set; }
        public string  ProductSubCategoryName { get; set; }

        //product category
        public long  ProductCategoryID { get; set; }

        //product
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string PhotoPath { get; set; }
        public string PartyCode { get; set; }
        public string Barcode { get; set; }
        public string Logo { get; set; }
        public long RitemId { get; set; }

        //productsize
        public string ProductSizeName { get; set; }

        //color
        public string CLName { get; set; }

        //ritem
        //public string RitemName { get; set; }
        //public string RitemCode { get; set; }
         
        public int ReceiveQty { get; set; }
        public int PackingQty { get; set; }


    }
}
