using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ProductionProcessDetails
    {
        public int ProcessID { get; set; }
        public string ProcessName { get; set; }
        public string Description { get; set; }
        public DateTime  EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public int IsRMRequired { get; set; }

        public int IsChargeable { get; set; }
        public int Charge_on_Item_Component { get; set; }
    }
}
