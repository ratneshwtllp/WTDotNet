using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class PrintDetails
    {
        public int PrintId { get; set; }
        public string Print { get; set; } 
        public string PrintDesc { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
    }
}
