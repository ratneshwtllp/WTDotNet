using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public class EMPGenderDetails
    {
        public int GenderId { get; set; }
        public string GenderName { get; set; }
        public string GenderDesc { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
