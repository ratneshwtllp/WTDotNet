using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ReciptsFromBuyerMaster
    {
        public long ReciptsId { get; set; }
        public long? ReciptsNo { get; set; }
        public string ReciptsNo1 { get; set; }
        public DateTime? ReciptsDate { get; set; }
        public int? BuyerId { get; set; }
        public int? MadiatorId { get; set; }
        public string Remarks { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public string AwbNo { get; set; }
        public int? CourierId { get; set; }
    }
}
