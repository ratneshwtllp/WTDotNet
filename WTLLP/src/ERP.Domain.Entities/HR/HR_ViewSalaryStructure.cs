using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class HR_ViewSalaryStructure
    {
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public long SalStrId { get; set; }
        public double Total { get; set; }
        public long SalStrChildId { get; set; }
        public int AllowanceId { get; set; }
        public double AllowanceAmount { get; set; }
        public string AllowanceType { get; set; }

        public int EPF_Employee { get; set; }
        public double  EPF_Emp_Per { get; set; }
        public int ESI_Employee { get; set; }
        public double  ESI_Emp_Per { get; set; }
        public int Dedu_Type { get; set; }
        public int FPF_Employer { get; set; }
        public double  FPF_Employer_Per { get; set; }

        public int ESI_Employer { get; set; }
        public double  ESI_Employer_Per { get; set; }
        public int Contri_Employer { get; set; }
        public double  Emp_Contri_Per { get; set; }
        public int Contri_type { get; set; }
    }
}
