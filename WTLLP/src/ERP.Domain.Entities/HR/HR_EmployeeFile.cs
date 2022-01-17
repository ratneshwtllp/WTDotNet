using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public class HR_EmployeeFile
    {
        public long EmployeeFileId { get; set; }
        public long EmployeeId { get; set; }
        public string FileLocation { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }
        public int IsPhoto { get; set; }
        public int UserId { get; set; }
        public string SessionYear { get; set; }
    }
}