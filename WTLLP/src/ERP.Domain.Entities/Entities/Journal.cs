using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class Journal
    {
        public long SNo { get; set; }
        public long Vno { get; set; }
        public string Vtype { get; set; }
        public DateTime Vdate { get; set; }
        public long Lfno { get; set; }
        public string DrCr { get; set; }
        public int LadgerNo { get; set; }
        public decimal DrAmount { get; set; }
        public decimal CrAmount { get; set; }
        public int ToAcc { get; set; }
        public int ByAcc { get; set; }
        public string FinYear { get; set; }
        public string Narration { get; set; }
        public int MonthId { get; set; }
        public string InvNo { get; set; }
        public string InvType { get; set; }
        public string Chqno { get; set; }
        public string Billno { get; set; }
        public DateTime? Bankdate { get; set; }
        public long? Vid { get; set; }
    }
}
