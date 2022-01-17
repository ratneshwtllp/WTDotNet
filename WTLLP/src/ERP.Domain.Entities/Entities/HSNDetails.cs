using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class HSNDetails
    {
        public int HSNId { get; set; }
        public int TaxId { get; set; }
        public string HSNCode { get; set; }
        public string  Remark { get; set; }
    }
}
