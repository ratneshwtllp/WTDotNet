using System;
namespace ERP.Domain.Entities
{
    public partial class HR_ViewAdvanceDetails
    {
        public long AdvanceId { get; set; }
        public long EmployeeId { get; set; }
        public DateTime AdvanceDate { get; set; }
        public double  AdvanceAmount { get; set; }
        public string Remark { get; set; }
        public string EmployeeCode { get; set; }
        public string  EmployeeName { get; set; }
        
       
    }
}
