using System;

namespace ERP.Domain.Entities
{
    public class ViewSaleOrderDetails
    {
        public long OrderMasterID { get; set; }
        public long OrderChildID { get; set; }
        public long FitemId { get; set; }
        public string Name { get; set; }
        public string  Code { get; set; }
        public string Description { get; set; }
        public long ProductSubCategoryID { get; set; }
        public string  ProductSubCategoryName { get; set; }
        public long  ProductCategoryID { get; set; }
        public int Qty { get; set; }
        public int  Size { get; set; }               //sizeid
        public string ProductSizeName { get; set; }

        public double Price { get; set; }
        public double  Amount { get; set; }
        public string CLName { get; set; }

        public string PartyCode { get; set; }
        public string Barcode { get; set; }
        public string Logo { get; set; }
        public string Unit { get; set; } 

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

        public string RitemName { get; set; }
        public string RitemCode { get; set; }
        public long RitemId { get; set; }

        public int BalanceQtyForWorkPlan { get; set; }

        public string PhotoPath { get; set; }

        public string CName { get; set; }
        public string CSName { get; set; }
        public int CurrencyID { get; set; }
         
        public string OrderChildRemark { get; set; }
        public DateTime ChildDeliveryDate { get; set; }
        public int ChildTransportId { get; set; }
        public string ChildTranportName { get; set; }
        public int SNo { get; set; }
        public int MultipleTransport { get; set; }
        public int MultipleDeliveryDate { get; set; }
        public DateTime ChildMyDeliveryDate { get; set; }
         
        public string FOBCIF { get; set; }
        public int TransportId { get; set; }
        public string TranportName { get; set; }
         
        public string CompanyName { get; set; }
        public string AddressWork { get; set; }
        public string Phone { get; set; }
        public string GSTN { get; set; }
        public DateTime MyDeliveryDate { get; set; }
        public string TransportCommments { get; set; }
        public int IsShoesOrder { get; set; }
        public string Paper1 { get; set; }
        public string Paper2 { get; set; }
        public string Fabric1 { get; set; }
        public string Fabric2 { get; set; }
        public string PRDWidth { get; set; }
        public string Thickness { get; set; }

        public int WPQty { get; set; }               //sizeid
        public string WPRemark { get; set; }
        public string GenderName { get; set; }
    }
}
