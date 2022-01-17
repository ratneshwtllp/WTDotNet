using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class InsertRecord
    {
        public int NumberId { get; set; }
        public string TextRemm { get; set; }
        public DateTime DateItime { get; set; }
    }
}
