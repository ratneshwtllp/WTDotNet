using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.Bank.Models
{
    public class BankModel
    {
        public int BankId { get; set; }
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }
    }
}