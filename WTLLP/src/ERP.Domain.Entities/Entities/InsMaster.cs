using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class InsMaster
    {
        public long InsId { get; set; }
        public long OrderId { get; set; }
        public DateTime InsDate { get; set; }
        public string InsBy { get; set; }
        public string Assorment { get; set; }
    }
}
