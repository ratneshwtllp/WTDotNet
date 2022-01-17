using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class RItemQuickTest 
    {
        public long RItemQuickTestId { get; set; } 
        public long RItem_Id { get; set; }
        public int  QuickTestId { get; set; } 
        public virtual RitemMaster RitemMaster { get; set; }

    }
}
