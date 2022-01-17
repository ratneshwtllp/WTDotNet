using System;

namespace ERP.Domain.Entities
{
    public class AttandanceSearchModel
    {
        public bool IsEmployee { get; set; }
        public long EmployeeId { get; set; }
        public bool IsDate { get; set; }
        public DateTime AttandanceDateFrom { get; set; }
        public DateTime AttandanceDateTo { get; set; }
        public bool IsMaxDate { get; set; }
        public int MonthId { get; set; }
        public int YearName { get; set; }
        public long DepartmentID { get; set; }
    }
}
