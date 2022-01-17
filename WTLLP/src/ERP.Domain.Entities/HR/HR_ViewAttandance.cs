using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class HR_ViewAttandance
    {
        public long EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public long AttandanceID { get; set; }
        public DateTime AttandanceDate { get; set; }        
        public string AttandanceStatus { get; set; }
        public string AttandanceType { get; set; }
        public int INHRS { get; set; }
        public int INMIN { get; set; }
        public string INAMPM { get; set; }
        public int OUTHRS { get; set; }
        public int OUTMIN { get; set; }
        public string OUTAMPM { get; set; }
        public int Lunch { get; set; }
        public int WHRS { get; set; }
        public int WMIN { get; set; }
        public long DepartmentID { get; set; }
    }
}

