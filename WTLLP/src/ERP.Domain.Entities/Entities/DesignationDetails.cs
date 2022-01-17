using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class DesignationDetails
    {
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
    }
}
