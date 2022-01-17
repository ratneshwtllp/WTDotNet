using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewProductProcess
    {
        public string ProcessName { get; set; } 
        public long ProductProcessID { get; set; }
        public long FitemId { get; set; }
        public long ProcessID { get; set; }
        public int SNo { get; set; }
        public DateTime EntryDate { get; set; }
        public int UserId { get; set; }
        public int IsChargeable { get; set; }
        public int Charge_on_Item_Component { get; set; }
    }
}
