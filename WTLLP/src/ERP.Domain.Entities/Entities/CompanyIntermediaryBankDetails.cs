using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class CompanyIntermediaryBankDetails
    {
        public int IntermediaryBankID { get; set; }
        public int CompanyID { get; set; }
        public int BankID { get; set; }
        public string BankAddress { get; set; }
        public string SwiftID { get; set; }
        public string RoutingNumber { get; set; }
        public string Remark { get; set; }
        public int UserID { get; set; }
    }
}
