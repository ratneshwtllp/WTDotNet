using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class SoleMaster
    {
        public int SoleId { get; set; }
        public string SoleName { get; set; }
        public string Remark { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
