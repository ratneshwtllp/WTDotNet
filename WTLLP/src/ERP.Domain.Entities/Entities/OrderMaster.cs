using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class OrderMaster
    {
        public int OrderId { get; set; }
        public int? SmsBulk { get; set; }
        public string OrderNo { get; set; }
        public string CustOrderNo { get; set; }
        public DateTime Date { get; set; }
        public DateTime ProdApprovalKeyDate { get; set; }
        public DateTime ProdStatrtingKeyDate { get; set; }
        public DateTime ShipmentApprovlKeyDate { get; set; }
        public int IssuedBy { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public double? MinDelTime { get; set; }
        public int? Transport { get; set; }
        public string TransportComment { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public int? PurchaseRead { get; set; }
        public string Comments { get; set; }
        public int? Instruction { get; set; }
        public int? AfterNoOfDays { get; set; }
        public int? MaterialStatus { get; set; }
        public DateTime? EntryModifyDate { get; set; }
        public int? MaterialRead { get; set; }
        public DateTime? OrderEntryDate { get; set; }
        public int? OrderReadAfterPoread { get; set; }
        public string ReasonForModify { get; set; }
        public int? TransportChange { get; set; }
        public string TransportChangeReason { get; set; }
        public DateTime? TransportChangeDate { get; set; }
        public string MaterialChangeComments { get; set; }
        public string FobCif { get; set; }
        public int? DeliveryDateChange { get; set; }
        public string DeliveryDateChangeReason { get; set; }
        public DateTime? DeliveryDateChangeDate { get; set; }
    }
}
