using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ReturnToSupplier
    {
        public long RTSID { get; set; }
        public DateTime RTSDate { get; set; }
        public long RTSSNo { get; set; }
        public string RTSNo { get; set; }
        public long SupplierId { get; set; }
        public int SuppAddressId { get; set; }
        public long RItem_Id { get; set; }
        public long RackId { get; set; }
        public double RTSQty { get; set; }
        public double Side { get; set; }
        public string Unit { get; set; }
        public string RTSRemark { get; set; }
        public long POID { get; set; }
        public string PONO { get; set; }
        public int RTSStatus { get; set; }
        public DateTime RTSStatusDate { get; set; }
        public int RTSStatus_Userid { get; set; }
        public string SessionYear { get; set; }
        public int userId { get; set; }
        public DateTime RTSEntryDate { get; set; }
        public string LotNo { get; set; }
    }
}
