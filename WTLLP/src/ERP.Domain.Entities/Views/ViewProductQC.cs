using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewProductQC 
    {
        public long QCID { get; set; }
        public long FitemId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long QCPointID { get; set; }
        public string QCPoint { get; set; }
        public string StandardValue { get; set; }
        public long ProductSubCategoryID { get; set; }
        public string ProductSubCategoryName { get; set; }
        public long ProductCategoryID { get; set; }
        public string ProductCategoryName { get; set; }
    }
}
