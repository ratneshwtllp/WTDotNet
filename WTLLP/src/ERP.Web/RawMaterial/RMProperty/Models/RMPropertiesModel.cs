using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.RMProperty.Models
{
    public class RMPropertiesModel
    {
        public int RMPropertiesID { get; set; }
        [Display(Name = "RMP Name")]
        public string RMPropertiesName { get; set; }
        [Display(Name = "Remark")]
        public string RMPropertiesRemark { get; set; }
        [Display(Name = "Printable On PO ?")]        
        public int RMPropertiesIsPrintOnPo { get; set; }
        public SelectList IsPrintOnPoList { set; get; }
        [Display(Name = "As a Master ?")]
        public int RMPropertiesIsMaster { get; set; }
        public SelectList IsMasterList { set; get; }
        public bool Selected { get; set; }
        public bool EditMode { get; set; }
        public long CategoryID { get; set; }
        public long RMPropertiesValueID { get; set; }
        public SelectList RMPValueList { get; set; }
        public string  RMPValue { get; set; }
        public int IsRequired { get; set; }
        public SelectList IsRequiredList { set; get; }
    }    
}