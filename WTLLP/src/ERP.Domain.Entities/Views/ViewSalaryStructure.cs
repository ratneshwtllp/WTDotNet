using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewSalaryStructure
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
    }
}
