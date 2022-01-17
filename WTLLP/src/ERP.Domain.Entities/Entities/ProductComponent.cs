using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ProductComponent
    {  
        public long ProductComponentId { get; set; }   
        public long FitemId { get; set; }
        public int Comp_Id { get; set; } 
        public int SNo { get; set; }
        public DateTime EntryDate { get; set; }
        public int UserId { get; set; }

        public virtual ProductDetails ProductDetails { get; set; }
    }
}
