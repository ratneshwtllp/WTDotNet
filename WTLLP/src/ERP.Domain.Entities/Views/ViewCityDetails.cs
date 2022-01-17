using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewCityDetails
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int StateId { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
    }
}
