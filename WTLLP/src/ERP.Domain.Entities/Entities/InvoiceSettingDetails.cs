using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class InvoiceSettingDetails
    {
        public long InvoiceSettingID { get; set; }
        public string Declaration1 { get; set; }
        public string Declaration2 { get; set; }
        public string Declaration3 { get; set; }
        public string InvoicePrefix { get; set; }
        public string Prop_authorize { get; set; }
        public string BankDetails { get; set; }
    }
}
