using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class UnitDetails
    {
        public int Uid { get; set; }
        public string Uname { get; set; }
        public string Usname { get; set; }
        public string Udesc { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
