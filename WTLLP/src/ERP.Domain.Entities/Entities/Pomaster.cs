using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class POMaster
    {
        public long Poid { get; set; }
        public string Pono { get; set; }
        public DateTime Podate { get; set; }
        public DateTime Dldate { get; set; }
        public long Sid { get; set; }
        public string Mode { get; set; }
        public string Sref { get; set; }
        public string Orf { get; set; }
        public string Dispatch { get; set; }
        public string Destination { get; set; }
        public string Terms { get; set; }
        public string Remark { get; set; }
        public double Amount { get; set; }
        public string Amtwords { get; set; }
        public string SessionYear { get; set; }
        public long UserId { get; set; }
        public int? PoAgainstOrder { get; set; }
        public int? IssuedBy { get; set; }
        public long? OrderId { get; set; }
        public long? ItemId { get; set; }
        public string ItemCode { get; set; }
        public int? ChangeDilervery { get; set; }
        public DateTime? NewDate { get; set; }
        public string ReasonForChange { get; set; }
        public short? CancelStatus { get; set; }
        public string ReasonForModify { get; set; }
        public string MultipleOrders { get; set; }
        public int POSerial { get; set; }
        public int TaxId { get; set; }
        public double RMTotal { get; set; }
        public double TaxAmount { get; set; }
        public double SH { get; set; }
        public double Other { get; set; }
        public double RoundOff { get; set; }
        public double NetAmount { get; set; }
        public double Freight { get; set; }
        public int EmailStatus { get; set; }
        public DateTime EmailDate { get; set; }
        public int Complete { get; set; }

        public string PaymentTerm { get; set; }
        public string DeliveryTerm { get; set; }
        public string Transport { get; set; }

        public string Declaration { get; set; }
        public string ForAnyQuery { get; set; }
        public double TotalIGST { get; set; }
        public double TotalCGST { get; set; }
        public double TotalSGST { get; set; }
        public long BranchId { get; set; }
        public double TotalAmtAfterTax { get; set; }

        public long BuyerId { get; set; }

        public virtual ICollection<POChild> POChild { get; set; }
        public virtual ICollection<POForSaleOrderDetails> POForSaleOrderDetails { get; set; }
    }
}
