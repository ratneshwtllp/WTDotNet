using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ComponentGroupDetails
    {
        public int CompGroupId { get; set; }
        public string CompGroupName { get; set; }
        public string CompGroupRemark { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
        public DateTime EntryDate { get; set; }
       
        
    }
}
