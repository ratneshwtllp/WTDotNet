using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class HR_DepartmentDetails
    {
        public long DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public string Description { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
