using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class LabourTypeDetails
    {
        public int Id { get; set; }
        public string LabourType { get; set; }
        public string Description { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
