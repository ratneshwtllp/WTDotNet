using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewStateDetails
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int StateId { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
    }
}
