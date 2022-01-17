using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.RMProperty.Models
{
    public class MappingModel
    {
        public long RMMappingID { get; set; }
        public long CategoryID { get; set; }
        public SelectList RMCategoryList { set; get; }
        public long RMPropertiesID { get; set; }
        public string RMPropertiesName { get; set; }
        public List<RMPropertiesModel> RMPropertiesModel { get; set; }      
        public bool Selected { get; set; }
        public int IsRequired { get; set; }
        public SelectList IsRequiredList { set; get; }
    }    
}