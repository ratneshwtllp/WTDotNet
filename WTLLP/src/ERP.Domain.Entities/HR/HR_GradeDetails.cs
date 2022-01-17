using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class HR_GradeDetails
    {
        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public string GradeCode { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
