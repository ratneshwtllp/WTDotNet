using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.Sole.Models
{
    public class SoleModel
    {
        public int SoleId { get; set; }
        [Display(Name = "Sole Name")]
        public string SoleName { get; set; }
        [Display(Name = "Remark")]
        public string Remark { get; set; }
    }
}