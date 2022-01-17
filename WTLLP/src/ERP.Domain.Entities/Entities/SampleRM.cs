using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class SampleRM
    {
        public long SampleRMId { get; set; }
        public long FitemId { get; set; }
        public long RitemId { get; set; }

        public virtual ProductDetails ProductDetails { get; set; }


    }
}
