using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class BranchDetails
    {
        public long BranchID { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
    }
}
