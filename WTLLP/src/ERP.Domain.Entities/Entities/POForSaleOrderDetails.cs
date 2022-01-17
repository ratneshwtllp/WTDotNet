using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class POForSaleOrderDetails
    {
        public long POForSaleOrderId { get; set; }
        public long Poid { get; set; }       
        public long OrderMasterId { get; set; }

        public virtual POMaster POMaster { get; set; }
    }
}
