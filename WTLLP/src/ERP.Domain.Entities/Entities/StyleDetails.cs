using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class StyleDetails
    { 
        public int StyleID { get; set; }
        public string StyleName { get; set; } 
        public string Description { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserID { get; set; }
    }
}
