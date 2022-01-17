using System;

namespace ERP.Domain.Entities
{
    public class ViewWPQtyForNextProcess
    {
        public long PlanChildId { get; set; }
        public int ProcessID { get; set; }
        public int QtyForNextProcess { get; set; }
    }
}
