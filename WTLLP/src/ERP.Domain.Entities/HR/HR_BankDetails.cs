using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class HR_BankDetails
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
      
    }
}
