using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ZipperMaster
    {
        public int ZipperId { get; set; }
        public string ZipperName { get; set; }
        public string Remark { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
