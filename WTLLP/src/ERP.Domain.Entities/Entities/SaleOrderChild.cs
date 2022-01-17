using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class SaleOrderChild
    {       
        public long OrderChildID { get; set; }
        public long OrderMasterID { get; set; }
        public long FitemId { get; set; }
        public int Qty { get; set; }
        public int  Size { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }

        public string OrderChildRemark { get; set; }
        public DateTime ChildDeliveryDate { get; set; }
        public int TransportId { get; set; }
        public int SNo { get; set; }
        public DateTime ChildMyDeliveryDate { get; set; }

        public virtual SaleOrderMaster SaleOrderMaster { get; set; }
    }
}
