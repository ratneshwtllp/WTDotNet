using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewContractorBalance
    {  
        public long ContractorID { get; set; }
        public string ContractorName { get; set; }  
        public double Balance { get; set; }
    }
}
