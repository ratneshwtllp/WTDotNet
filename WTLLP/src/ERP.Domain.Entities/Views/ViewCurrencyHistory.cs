using System;
namespace ERP.Domain.Entities
{
    public class ViewCurrencyHistory
    {
        public long CurrencyHistoryId { get; set; }
        public int Cid { get; set; }
        public string Cname { get; set; }
        public string Csname { get; set; }
        public DateTime? ChangeDate { get; set; }
        public decimal PreviousPrice { get; set; }
        public decimal ChangePrice { get; set; }
    }

}
