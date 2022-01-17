using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewReserveStock
    {  
        //reservestockmaster
        public long ReserveId { get; set; }
        public string ReserveNo { get; set; }
        public DateTime ReserveDate { get; set; }
        public double ReserveQty { get; set; }
        public long SupplierId { get; set; }
        public long RitemId { get; set; }
        public long FitemId { get; set; }
        public long OrderId { get; set; }
        public int ReserveStatus { get; set; }
        //public string SessionYear { get; set; }
        //public int UserId { get; set; }
         
        //rm 
        public string Name { get; set; }
        public string Code { get; set; }
        public int CostUnit { get; set; }

        //unit
        public string USName { get; set; }

        //order
        public string OrderNo { get; set; }

        //buyer
        public long BuyerId { get; set; }
        public string PartyCode { get; set; }
        public string BuyerName { get; set; }

    }
}
