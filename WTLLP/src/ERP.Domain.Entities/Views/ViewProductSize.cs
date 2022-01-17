using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewProductSize
    {
        public long FitemId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long ProductMultipleSizeId { get; set; }
        public int SizeId { get; set; }
        public string SizeName { get; set; }
        public string SizeBarcode { get; set; }
        //public int CLID { get; set; }
        //public string CLName { get; set; }

    }
}
