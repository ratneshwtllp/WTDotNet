using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ExtraQtyPermission
    {
        public long MessegeId { get; set; }
        public string Pono { get; set; }
        public string Gipno { get; set; }
        public int? AdminResponse { get; set; }
        public string Messege { get; set; }
        public string ItemName { get; set; }
        public double? BalanceQty { get; set; }
        public double? ReceiveQty { get; set; }
        public int? UserId { get; set; }
        public DateTime? MessegeDate { get; set; }
        public int? GipRead { get; set; }
        public long? ItemId { get; set; }
    }
}
