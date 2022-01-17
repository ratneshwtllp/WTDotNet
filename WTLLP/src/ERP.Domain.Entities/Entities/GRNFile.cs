using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class GRNFile
    {
        public long GRNFileId { get; set; }
        public long GRNId { get; set; }
        public string FileLocation { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }
        public int IsPhoto { get; set; }
        public int UserId { get; set; }
        public string SessionYear { get; set; }
        public int DocumentTypeId { set; get; }
    }
}
