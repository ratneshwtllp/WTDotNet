using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.Buyer.Models
{
    public class BuyerBankModel
    {
     
        public long BuyerBankId { get; set; }
        public long BuyerId { get; set; }
        public string Beneficiary { get; set; }
        [Display(Name = "Bank Name")]
        public int BankId { get; set; }
        [Display(Name = "Bank Address")]
        public string BankAddress { get; set; }
        [Display(Name = "Account No")]
        public string AccountNo { get; set; }
        [Display(Name = "Bank SwiftCode")]
        public string BankSwiftCode { get; set; }
        [Display(Name = "Bank IFSC Code")]
        public string BankIFSCCode { get; set; }
        [Display(Name = "Bank Name")]
        public string  BankName { get; set; }
    }
}
