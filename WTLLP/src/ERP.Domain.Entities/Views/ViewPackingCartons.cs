using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewPackingCartons
    {
        public long PackingID { get; set; }
        public int CartonNo { get; set; }
        public int CartonID { get; set; }
        public double Weight { get; set; }
        public string Dimension { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Volume { get; set; }
        public long RowID { get; set; }         
    }
}
