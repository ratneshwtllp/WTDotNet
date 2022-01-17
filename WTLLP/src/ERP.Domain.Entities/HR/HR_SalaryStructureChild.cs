using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class HR_SalaryStructureChild
    {       
        public long SalStrChildId { get; set; }
        public long SalStrId { get; set; }
        public int AllowanceId { get; set; }
        public double AllowanceAmount { get; set; }
        
        public virtual HR_SalaryStructureMaster HR_SalaryStructureMaster { get; set; }
    }
}
