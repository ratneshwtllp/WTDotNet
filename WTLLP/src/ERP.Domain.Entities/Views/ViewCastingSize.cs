using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewCastingSize
    {
        public long FitemID { get; set; }
        public int  CLID { get; set; }
        public int SizeId { get; set; }
        public string SizeName { get; set; }
        public long  CostingID { get; set; }
    }
}
