using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class PFDetails
    {
        public long PFID { get; set; }
        public double PFLimit { get; set; }
        public double EPFEmployee { get; set; }
        public double FPFEmployer { get; set; }
        public double EmployerContribution { get; set; }
        public DateTime ApplyDate { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
    }
}
