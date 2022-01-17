using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class FinishMetalDetails
    {
        public int Mfid { get; set; }
        public string Mfname { get; set; }
        public string Description { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
