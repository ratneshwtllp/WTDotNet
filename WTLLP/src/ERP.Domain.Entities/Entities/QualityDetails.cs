using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class QualityDetails
    {
        public int QualityId { get; set; }
        public string QualityName { get; set; }
        public string QualityDesc { get; set; }
        public DateTime ? EntryDate { get; set; }
        public string Sessionyear { get; set; }
        public int? UserID { get; set; }
    }
}
