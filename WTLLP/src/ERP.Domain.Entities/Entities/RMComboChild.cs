using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class RMComboChild
    { 
        public long RMComboChildID { get; set; }
        public long RMComboID { get; set; }
        public long RitemId { get; set; }
        public long CompID { get; set; } 
        public int CompQty { get; set; }
        public int SerialNo { get; set; }
        public int CompGroupId { get; set; }
        public string Remark { get; set; }
        public string Photo { get; set; }
        public double RMQty { get; set; }
        public long SupplierId { get; set; }
        public int ProcessID { get; set; }

        public virtual RMComboMaster RMComboMaster { get; set; }
    }
}
