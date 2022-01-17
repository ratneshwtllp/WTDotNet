using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class CurrencyHistoryDetails
    {
        public Int64  CurrencyHistoryId { get; set; }
        public int CID { get; set;}
        public DateTime? ChangeDate { get; set; }
        public decimal PreviousPrice { get; set; }
        public decimal ChangePrice { get; set; }
        public int? UserId { get; set; }

        public virtual CurrencyDetails CurrencyDetails { get; set; }
    }
}
