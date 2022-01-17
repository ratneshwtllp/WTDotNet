using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class FormNumberSettingsDetails
    {
        public int SNo { set; get; }
        public string FormName { set; get; }
        public string Prefix { set; get; }
        public string SessionYear { set; get; }
        public int StartingNumber { set; get; }
        public int NoOfDigits { set; get; }
        public string DispalyNumber { set; get; }
        public DateTime EntryDate { get; set; }
        public int IsBatchOfRM { set; get; }
    }
}
