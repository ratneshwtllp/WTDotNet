using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class Move_RMStock
    { 
        public long MoveRMStockID { get; set; }
        public long RitemID_From { get; set; }
        public long SupplierID_From { get; set; }
        public long RackID_From { get; set; }
        public long RitemID_To { get; set; }
        public long SupplierID_To { get; set; }
        public long RackID_To { get; set; }
        public double Quantity { get; set; }
        public string Remark { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public string LotNo { get; set; }        
    }
}
