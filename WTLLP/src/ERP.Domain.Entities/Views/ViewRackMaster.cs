using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewRackMaster
    {
        public long RackId { get; set; }
        public string RackNo { get; set; }
        public long RackSNo { get; set; }
        public string LineNo { get; set; }
        public string LineCode { get; set; }
        public long LineSNo { get; set; }
        public string Remark { get; set; }
        public long StoreId { get; set; }
        public string StoreName { get; set; }
    }
}
