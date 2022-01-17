using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewProductComponent
    {
        
        public long ProductComponentId { get; set; }
        public int SNo { get; set; }
        public string Code { get; set; }
        public int Comp_Id { get; set; }
        public string Comp_Name { get; set; }
        public long FitemId { get; set; }
    }
}
