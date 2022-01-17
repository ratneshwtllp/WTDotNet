using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class EmbossmentDetails
    {
        public int EmbossmentId { get; set; }
        public string EmbossmentName { get; set; }
        public string EmbossmentDesc { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserID { get; set; }
    }
}
