using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class StateDetails
    {
        public StateDetails()
        {
            CityDetails = new HashSet<CityDetails>();
        }

        public int StateId { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public int CountryId { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserID { get; set; }

        public virtual CountryDetails Country { get; set; }

        public virtual ICollection<CityDetails> CityDetails { get; set; }
    }
}
