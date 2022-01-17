using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class GoodsReceivedMaster
    {
        public long GreceiveId { get; set; }
        public long SoId { get; set; }
        public long JobCardId { get; set; }
        public DateTime GreceiveDate { get; set; }
        public string GreceivedBy { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
