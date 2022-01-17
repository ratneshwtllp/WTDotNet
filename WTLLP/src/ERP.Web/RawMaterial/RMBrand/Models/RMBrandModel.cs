using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.RMBrand.Models
{
    public class RMBrandModel
    {
        public int RMBrandId { get; set; }
        [Display(Name = "RMBrand Name")]
        public string RMBrandName { get; set; }
        [Display(Name = "RMBrand Code")]
        public string RMBrandCode { get; set; }
        public string RMBrandDesc { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
         
        //public long BuyerId { get; set; }
        //[Display(Name = "Buyer")]
        //public string BuyerCode { get; set; }
        //public SelectList BuyerList { set; get; }
    }
}