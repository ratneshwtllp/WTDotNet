using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class UserAsSupervisorDetails
    {
        public long UserAsSupervisorId { get; set; }
        public int UserId { get; set; }
        public long ContractorID { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int LoginUserId { get; set; }
      
    }
}
