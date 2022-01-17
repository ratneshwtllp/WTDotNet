using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class GroupDetails
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupDesc { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserID { get; set; }
    }
}
