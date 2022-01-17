using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.RMProperty.Models
{
    public class RMPValueModel
    {
        public long RMPropertiesValueID { get; set; }
        [Display(Name = "RMP Name")]
        public int RMPropertiesValId { get; set; }
        public SelectList RMPropertiesValueList { set; get; }
        [Display(Name = "RMP Value")]
        public string RMPropertiesValue { get; set; }
        [Display(Name = "Remark")]
        public string RMPropertiesRemark { get; set; }            
        public string Session_Year { get; set; }
        public int UserId { get; set; }
        public DateTime EntryDate { get; set; }
        public bool EditMode { get; set; }
    }    
}