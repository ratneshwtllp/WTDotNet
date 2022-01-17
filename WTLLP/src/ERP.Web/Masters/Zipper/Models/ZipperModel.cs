using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.Zipper.Models
{
    public class ZipperModel
    {
        public int ZipperId { get; set; }
        [Display(Name = "Zipper Name")]
        public string ZipperName { get; set; }
        [Display(Name = "Remark")]
        public string Remark { get; set; }
    }
}