using System;
using System.Collections.Generic;
namespace ERP.Domain.Entities
{
    public partial class BuyerBankDetails
    {
        public long BuyerBankId {get; set;}
        public long BuyerId { get;set;}
        public string Beneficiary {get; set;}
        public int BankId {get; set;}
        public string BankAddress {get; set;}
        public string AccountNo { get; set;}
        public string  BankSwiftCode { get; set;}
        public string  BankIFSCCode { get;set;}
        public DateTime EntryDate { get;set;}
        public string SessionYear { get;set;}
        public int UserId {get; set;}
      
    }
}
