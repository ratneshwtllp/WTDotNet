using System;

namespace ERP.Domain.Entities
{
    public class ViewPOMaster
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

        public double RMTotal { get; set; }
        public double TaxAmount { get; set; }
        public double SH { get; set; }
        public double Other { get; set; }
        public double RoundOff { get; set; }
        public double NetAmount { get; set; }

        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }

        public double Freight { get; set; }

        public int EmailStatus { get; set; }
        
        public string SupplierEmail { get; set; }
        public DateTime EmailDate{ get; set; }


    }
}
