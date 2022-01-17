using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class NropCpMaster
    {
        public long NropTableId { get; set; }
        public long NropId { get; set; }
        public string NropNo { get; set; }
        public DateTime NropDate { get; set; }
        public string Comments { get; set; }
        public string OutPassBy { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
    }
}
