using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class HR_ViewCalculateMonthlySalary
    {
        public long SalStrId { get; set; }
        public string AllowanceType { get; set; }
        public int  AllowanceId { get; set; }
        public double  AllowanceAmount { get; set; }
        public int EPF_Employee { get; set; }
        public int  ESI_Employee { get; set; }
        public int Dedu_Type { get; set; }
        public int FPF_Employer { get; set; }
        public int ESI_Employer { get; set; }
        public int  Contri_Employer { get; set; }
        public long  EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string  EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public int PresentDays { get; set; }
        public int  AttMonth { get; set; }
        public int  AttYear { get; set; }
        public double  Total { get; set; }
        //public double  PardaySalery { get; set; }
        public double  MonthlSalary { get; set; }
        public long SalStrChildId { get; set; }
        public int WorkingDays { get; set; }
        public double  MonthAllowance { get; set; }
        public long DepartmentID { get; set; }
    }
}
