using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ActualRateRmhistory
    {
        public int RmHistoryId { get; set; }
        public DateTime Date { get; set; }
        public int RmId { get; set; }
        public string PurchaseUnit { get; set; }
        public decimal PurchasePrice { get; set; }
        public string CostingUnit { get; set; }
        public decimal CostingPrice { get; set; }
        public double Conversion { get; set; }
    }
}
