using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewBuyerBank
    {
        public long BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; }
        public long BuyerBankId { get; set; }
        public string Beneficiary { get; set; }
        public int BankId { get; set; }
        public string BankAddress { get; set; }
        public string AccountNo { get; set; }
        public string BankSwiftCode { get; set; }
        public string BankIFSCCode { get; set; }
        public string BankName { get; set; }
    }
}
