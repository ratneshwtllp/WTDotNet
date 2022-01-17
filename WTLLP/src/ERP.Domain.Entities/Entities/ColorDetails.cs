using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ColorDetails
    {
        public int Clid { get; set; }
        public string Clname { get; set; }
        public string Description { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
        public int Online_Transfer { get; set; }
    }
}
