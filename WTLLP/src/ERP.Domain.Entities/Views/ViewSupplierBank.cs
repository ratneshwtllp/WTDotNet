using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewSupplierBank
    {
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
        public long SupplierBankId { get; set; }
        public string Beneficiary { get; set; }
        public int BankId { get; set; }
        public string BankAddress { get; set; }
        public string AccountNo { get; set; }
        public string BankSwiftCode { get; set; }
        public string BankIFSCCode { get; set; }
        public string BankName { get; set; }
        public string ContactPerson { get; set; }
        public string SupplierAddress { get; set; }
    }
}
