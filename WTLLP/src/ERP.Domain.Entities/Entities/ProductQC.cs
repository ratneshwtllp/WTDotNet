using System;
namespace ERP.Domain.Entities
{
    public partial class ProductQC 
    {
        public long QCID { get; set; }
        public long QCPointID { get; set; }
        public long FitemId { get; set; }
        public string StandardValue { get; set; }
        
    }
}
