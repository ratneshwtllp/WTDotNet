using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class MenOutPass
    {
        public long MenopId { get; set; }
        public int MenopSerialNo { get; set; }
        public string MenopNo { get; set; }
        public DateTime MenopDate { get; set; }
        public int StaffContractor { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
    }
}
