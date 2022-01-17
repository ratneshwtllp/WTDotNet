using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class HR_AllowanceDetails
    {
        public int AllowanceId { get; set; }
        public string AllowanceType { get; set; }
        public string Description { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
