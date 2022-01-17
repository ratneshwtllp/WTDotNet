using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public class PatternRoomDetails
    {
        public long PatternRoomId { get; set; }
        public long FItemId { get; set; }
        public int StoreQty { get; set; }
        public DateTime StoreDate { get; set; }
        public int RackNo { get; set; }
        public string Session_Year { get; set; }
        public int? UserId { get; set; }
        public int ComponentQty { get; set; }

    }
}
