using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class OrderShippment
    {
        public long ShipId { get; set; }
        public long OrderId { get; set; }
        public DateTime ShipDate { get; set; }
        public string Remark { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
    }
}
