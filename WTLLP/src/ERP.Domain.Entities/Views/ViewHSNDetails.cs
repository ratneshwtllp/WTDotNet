using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewHSNDetails
    {
        public int TaxId { get; set; }
        public string TaxName { get; set; }
        public double TaxPer { get; set; }
        public string TaxFullName { get; set; }
        public int HSNId { get; set; }
        public string  HSNCode { get; set; }
        public string Remark { get; set; }
    }
}
