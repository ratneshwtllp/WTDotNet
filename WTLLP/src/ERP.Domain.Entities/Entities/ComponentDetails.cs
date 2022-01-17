using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ComponentDetails
    {
        public int CompId { get; set; }
        public string CompName { get; set; }
        public string CompDescr { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
        public int?CompGroupId { get; set; }
    }
}
