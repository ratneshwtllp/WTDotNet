using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class HR_SalaryStructureMaster
    {
        public long SalStrId { get; set; }
        public long EmployeeId { get; set; }
        public int EPF_Employee { get; set; }
        public int ESI_Employee { get; set; }
        public int Dedu_Type { get; set; }
        public int FPF_Employer { get; set; }
        public int ESI_Employer { get; set; }
        public int Contri_Employer { get; set; }
        public int Contri_type { get; set; }
        public string Session_year { get; set; }
        public int? UserID { get; set; }
        public DateTime EntryDate { get; set; }
        public double Total { get; set; }

        public double EPF_Emp_Per { get; set; }
        public double ESI_Emp_Per { get; set; }
        public double FPF_Employer_Per { get; set; }
        public double ESI_Employer_Per { get; set; }
        public double Emp_Contri_Per { get; set; }
    

        public virtual ICollection<HR_SalaryStructureChild> HR_SalaryStructureChild { get; set; }
    }
}
