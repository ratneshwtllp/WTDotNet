using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class NonRetunableOutPassFitem
    {
        public int ReturnableId { get; set; }
        public int Sno { get; set; }
        public string OutPassNo { get; set; }
        public DateTime Date { get; set; }
        public string IssueTo { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
    }
}
