using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class RMChild
    {
        public long RMChildId { get; set; }
        public long RItem_Id { get; set; }
        public int RMPropertiesID { get; set; }
        public long RMPropertiesValueID { get; set; }
        public string  RMPValue { get; set; }
        public virtual RitemMaster RitemMaster { get; set; }

    }
}
