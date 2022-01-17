using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class LiningDetails
    {
        public int LiningID { get; set; }
        public string LiningName { get; set; }
        public string LiningDesc { get; set; }
        public DateTime ? EntryDate { get; set; }
        public string Sessionyear { get; set; }
        public int? UserID { get; set; }
    }
}
