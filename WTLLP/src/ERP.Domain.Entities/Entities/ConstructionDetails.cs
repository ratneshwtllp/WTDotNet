using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ConstructionDetails
    {
        public int ConstrnId { get; set; }
        public string ConstrnName { get; set; }
        public string ConstrnDesc { get; set; } 
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
