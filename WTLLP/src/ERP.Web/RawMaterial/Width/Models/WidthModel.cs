using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.Width.Models
{
    public class WidthModel
    {
        public int WidthId { get; set; }

        [Display(Name = "Width Name")]
        public string WidthName { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Description")]
        public string WidthDesc { get; set; }

        //public long BuyerId { get; set; }
        //public string BuyerName { get; set; }

        public SelectList UnitList { set; get; }
    }
}