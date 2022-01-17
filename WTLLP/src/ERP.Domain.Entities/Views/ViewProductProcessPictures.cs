using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewProductProcessPictures
    {
        public long ProductProcessPicturesId { get; set; }
        public long FitemId { get; set; }
        public int ProcessId { get; set; }
        public string FileLocation { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
         
        public string Name { get; set; }
        public string Code { get; set; } 

        public string ProcessName { get; set; }
    }
}
