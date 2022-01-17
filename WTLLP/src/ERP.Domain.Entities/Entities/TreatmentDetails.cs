using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class TreatmentDetails
    {
        public int TreatmentId { get; set; }
        public string TreatmentName { get; set; }
        public string TreatmentDesc { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
