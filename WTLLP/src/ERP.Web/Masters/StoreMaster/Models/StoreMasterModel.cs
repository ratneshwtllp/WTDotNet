using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.StoreMaster.Models
{
    public class StoreMasterModel
    {
        public int StoreId { get; set; }
        [Display(Name = "Store Name")]
        public string StoreName { get; set; }
    }
}