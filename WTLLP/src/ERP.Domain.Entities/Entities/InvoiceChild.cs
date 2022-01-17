using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class InvoiceChild
    { 
        public long InvoiceChildID { get; set; }
        public long InvoiceID { get; set; }
        public long FitemID { get; set; }
        public int ProductSizeId { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public int SNo { get; set; }
         
        public virtual InvoiceMaster InvoiceMaster { get; set; }
    }
}  
