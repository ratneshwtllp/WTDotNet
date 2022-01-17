using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewCostingCurrencyDetails
    { 
        public long RowID { get; set; }
        public int CID { get; set; }
        public string CName { get; set; }
        public string CSName { get; set; }
        public decimal CRate { get; set; }
        public int TransportId { get; set; }
        public string TranportName { get; set; }
    }
}
