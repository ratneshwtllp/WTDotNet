using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.State.Models
{
    public class StateModel
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public SelectList CountryList { set; get; }
    }
}