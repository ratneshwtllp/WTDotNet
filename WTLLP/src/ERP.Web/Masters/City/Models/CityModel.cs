
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.City.Models
{
    public class CityModel
    {
        public int CityId { get; set; }
        public string CityName { get; set; }

        public int CountryId { get; set; }
        public SelectList CountryList { set; get; }

        public int StateId { get; set; }
        public SelectList StateList { set; get; }
    }
}