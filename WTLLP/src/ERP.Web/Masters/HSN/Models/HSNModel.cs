
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.HSN.Models
{
    public class HSNModel
    {
        public int HSNId { get; set; }
        public string HSNCode { get; set; }
        public int TaxId { get; set; }
        public SelectList TaxList { set; get; }
        public string  Remark { get; set; }
    }
}