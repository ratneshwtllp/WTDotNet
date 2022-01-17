using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class TypeOfColourDetails
    {
        public int TypeOfColourId { get; set; }
        public string TypeOfColour { get; set; }
        public string TypeOfColourDesc { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
