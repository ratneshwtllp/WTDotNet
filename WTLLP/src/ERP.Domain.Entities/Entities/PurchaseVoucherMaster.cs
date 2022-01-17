using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class PurchaseVoucherMaster
    {
        public long PVoucherId { get; set; }
        public long Sid { get; set; }
        public int AccNo { get; set; }
        public DateTime PVoucherDate { get; set; }
        public double TotalAmount { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public double? TaxTotal { get; set; }
        public double? Total { get; set; }
        public double? Roundoff { get; set; }
        public double? VoucherAmt { get; set; }
        public string Remark { get; set; }
        public string AmtInWords { get; set; }
        public int? SipOrDirect { get; set; }
        public string Billno { get; set; }
        public DateTime? BillDate { get; set; }
        public long? PVoucherSid { get; set; }
    }
}
