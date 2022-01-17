using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class HR_AdvanceDetails
    {
        public long AdvanceId { get; set; }
        public long EmployeeId { get; set; }
        public DateTime AdvanceDate { get; set; }
        public double  AdvanceAmount { get; set; }
        public string Remark { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        
       
    }
}
