using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class TaxDetails
    {
        public int TaxId { get; set; }
        public string TaxName { get; set; }
        public double TaxPer { get; set; }
        public string TaxFullName { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
