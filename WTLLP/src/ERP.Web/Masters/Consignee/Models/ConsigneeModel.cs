using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.Consignee.Models
{
    public class ConsigneeModel
    {
        public long ConsigneeId { get; set; }

        [Display(Name = "ConsigneeCode")]
        public string ConsigneeCode { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneeAddress { get; set; }

        //[Display(Name = "Country")]
        //public string Country { set; get; }
        public int CountryId { get; set; }
        public SelectList CountryList { set; get; }

        //[Display(Name = "State")]
        //public string State { get; set; }
        public int StateId { get; set; }
        public SelectList StateList { set; get; }

        //[Display(Name = "City")]
        //public string City { set; get; }
        public int CityId { get; set; }
        public SelectList CityList { set; get; }

        public string ConsigneePin { get; set; }
        public string ConsigneeEmail { get; set; }
        public string ConsigneeMobile { get; set; }
        public string ConsigneePhone { get; set; }
        
        
    }
}