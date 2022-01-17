using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.RMSubCat.Models
{
    public class RMSubCategoryModel
    {
        public long SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public string Description { get; set; }
        public long CategoryID { get; set; }
        public long Category { get; set; }
        public SelectList RMCategoryList { set; get; }
         
        [Display(Name = "HSN")]
        public int HSNId { get; set; }
        public SelectList HSNList { set; get; }
    }
}