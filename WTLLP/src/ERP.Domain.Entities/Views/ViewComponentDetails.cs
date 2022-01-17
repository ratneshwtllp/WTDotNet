using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewComponentDetails
    {
        public int Comp_Id { get; set; }
        public string Comp_Name { get; set; }
        public string Comp_Descr { get; set; }
        public int CompGroupId { get; set; }
        public string CompGroupName { get; set; }
       
    }
}
