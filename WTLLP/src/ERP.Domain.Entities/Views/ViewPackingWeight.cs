using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewPackingWeight
    {
        public long RowID { get; set; }
        public long PackingID { get; set; }
        public int TotalCarton { get; set; } 
        public string Dimension { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Volume { get; set; }

    }
}
