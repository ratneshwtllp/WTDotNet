using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class DocumentTypeDetails
    {
        public int DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }
        public string Remark { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
        public DateTime EntryDate { get; set; }
        
    }
}
