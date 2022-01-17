using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.Size.Models
{
    public class SizeModel
    {
        public int SizeId { get; set; }

        [Display(Name = "Size Name")]
        public string SizeName { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Description")]
        public string SizeDesc { get; set; }

        public SelectList UnitList { set; get; }
    }
}