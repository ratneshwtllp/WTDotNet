using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.RM.Models
{
    public partial class QuickTestModel 
    {
        public int ID { get; set; }
        public string TestName { get; set; } 
        public int MasterBelongTo { get; set; }
        public bool Selected { get; set; }
    }
}
