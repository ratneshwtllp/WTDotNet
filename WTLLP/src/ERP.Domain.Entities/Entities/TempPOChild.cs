using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class TempPOChild
    {        
        public int SNo { get; set; }
        public int RItem_Id { get; set; }
        public double Qty { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
        public string RDesc { get; set; }
        public long SupplierID { get; set; }
    }
}
