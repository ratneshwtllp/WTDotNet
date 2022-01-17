using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewSaleOrderStatus
    {
        public long BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; } 
        public long OrderMasterID { get; set; } 
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime MyDeliveryDate { get; set; }
        public int IsShoesOrder { get; set; }
        public long OrderChildID { get; set; }
        public long FitemId { get; set; }
        public int Qty { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long PlanId { get; set; }
        public DateTime PlanDate { get; set; }
        public DateTime DiliveryDatePlan { get; set; }
        public string ContractorName { get; set; }
        public int IsPending { get; set; }
        public string ProcessName { get; set; }
        public string PlanNo { get; set; }
        public int ProductSizeId { get; set; }
        public string ProductSizeName { get; set; }
        public long ContractorID { get; set; }
        public int PlanQty { get; set; }
        public string ProcessContactor { get; set; }
        public int NoOfDaysToOrderDelivery { get; set; }
        public int NoOfDaysToCompleteWP { get; set; }
        public long PlanChildId { get; set; }
        public int InQty { get; set; }
        public int OutQty { get; set; }
        public int ProcessID { get; set; }
        public string Unit { get; set; }
        public int ProcessBal { get; set; }
        public string PRRecvNo { get; set; }
        public DateTime PRRecvDate { get; set; }
        public string PRNo { get; set; }
        public DateTime PRDate { get; set; }
    }
}
