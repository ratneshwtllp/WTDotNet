using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class QuickTestDetails 
    {
        public int ID { get; set; }   
        public string TestName { get; set; }
        public int MasterBelongTo { get; set; } 
    
    }
}
