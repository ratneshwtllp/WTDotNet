using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewCompanyBeneficiaryBankDetails
    {
        public int BeneficiaryBankID { get; set; }
        public int CompanyID { get; set; }
        public string CName { get; set; }
        public int BankID { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string SwiftID { get; set; }
        public string AccountNumber { get; set; }
        public string Remark { get; set; }
      
    }
}
