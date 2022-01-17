using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class MchMaster
    {
        public long MchId { get; set; }
        public long MchSno { get; set; }
        public string MchNo { get; set; }
        public DateTime MchDate { get; set; }
        public int MchCurrencyId { get; set; }
        public long OrderId { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
    }
}
