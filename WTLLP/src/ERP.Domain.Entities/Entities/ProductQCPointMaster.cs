using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ProductQCPointMaster
    {
        public long QCPointID { get; set; }
        public string QCPoint { get; set; }
        public string QCRemark { get; set; }
        public DateTime? EntryDate { get; set; }
        public int? UserID { get; set; }
    }
}
