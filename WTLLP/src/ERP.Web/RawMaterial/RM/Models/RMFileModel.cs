using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.RM.Models
{
    public partial class RMFileModel
    { 
        public long RMFileId { get; set; }
        public long RitemId { get; set; }
        public string FileLocation { get; set; }
        public string FileName { get; set; }       
        public DateTime UploadDate { get; set; }
        public int IsPhoto { get; set; }
        public int UserId { get; set; }
        public string SessionYear { get; set; }
        public string DocumentTypeName { get; set; }
        public int DocumentTypeId { set; get; }
        public long SupplierId { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public int AlertInDays { get; set; }
        public string Remarks { get; set; }
        public int IsReveiew { get; set; }
        public DateTime ReviewDate { get; set; }
        public int ReviewStatus { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
    }
}