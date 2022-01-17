using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class WashDetails
    {
        public int WashID { get; set; }
        public string Wash { get; set; }
        public string Description { get; set; }
        public DateTime ? EntryDate { get; set; }
        public string Sessionyear  { get; set; }
        public int? UserID { get; set; }
    }
}
