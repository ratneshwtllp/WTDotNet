using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class RMFile
    {
        public long RMFileId { get; set; }
        public long RItem_Id { get; set; }
        public string FileLocation { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }
        public int IsPhoto { get; set; }
        public int UserId { get; set; }
        public string SessionYear { get; set; }
        public int DocumentTypeId { get; set; }
        public long SupplierId { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public int AlertInDays { get; set; }
        public string Remarks { get; set; }
        public int IsReveiew { get; set; }
        public DateTime ReviewDate { get; set; }
        public int ReviewStatus { get; set; }
    }
}
