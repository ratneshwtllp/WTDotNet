using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class CityDetails
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserID { get; set; }
        
        public virtual StateDetails State { get; set; }
    }
}
