using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class RItemSupp
    {
        public long RItemSuppID { get; set; }
        public long RItem_Id { get; set; }
        public long SupplierId { get; set; }
        public double Price { get; set; }
        public int MinDays { get; set; }
        public int SuppUnit { get; set; }
        public string SupplierRMCode { get; set; }

        //public string SessionYear { get; set; }
        //public int UserId { get; set; }
        //public DateTime EntryDate { get; set; }

        public virtual RitemMaster RitemMaster { get; set; }

    }
}
