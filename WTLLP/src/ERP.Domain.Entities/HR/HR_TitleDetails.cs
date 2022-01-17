using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class HR_TitleDetails
    {
        public int TitleId { get; set; }
        public string TitleName { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
