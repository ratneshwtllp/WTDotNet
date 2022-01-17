using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class CurrencyDetails
    {
        public int Cid { get; set;}
        public string Cname { get; set; }
        public string Csname { get; set; }
        public decimal Crate{ get; set; } 
        public DateTime? EntryDate{ get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }

        public virtual ICollection<CurrencyHistoryDetails> CurrencyHistoryDetails { get; set; }
    }
}
