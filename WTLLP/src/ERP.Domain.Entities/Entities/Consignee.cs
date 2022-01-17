using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class Consignee
    {
        public long ConsigneeId { get; set; }
        public string Ccode { get; set; }
        public string Cname { get; set; }
        public string Caddress { get; set; }
        public string Ccity { get; set; }
        public string Cstate { get; set; }
        public string Ccountry { get; set; }
        public string Czippin { get; set; }
        public string Cmobile { get; set; }
        public string Cphone { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
