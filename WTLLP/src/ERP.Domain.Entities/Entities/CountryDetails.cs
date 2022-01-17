using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class CountryDetails
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserID { get; set; }
    }
}
