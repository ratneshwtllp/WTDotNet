using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class CostingElementDetails
    {
        public int CostingElementId { get; set; }
        public string CostingElementName { get; set; }
        public string CostingElementDesc { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
