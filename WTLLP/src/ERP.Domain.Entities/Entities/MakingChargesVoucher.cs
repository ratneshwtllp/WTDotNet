using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class MakingChargesVoucher
    {
        public long VoucherId { get; set; }
        public DateTime VoucherDate { get; set; }
        public string JbcNo { get; set; }
        public long OrderId { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public string RecordNo { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public long WorkerId { get; set; }
        public double TdsAmount { get; set; }
        public long TodayQty { get; set; }
        public string AmtInWords { get; set; }
        public int VoucherSno { get; set; }
        public string VoucherNo { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
