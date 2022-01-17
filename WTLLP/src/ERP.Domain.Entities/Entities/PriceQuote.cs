using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class PriceQuote
    {
        public int PriceQuoteId { get; set; }
        public int? FitemId { get; set; }
        public int? BrandId { get; set; }
        public DateTime? Date { get; set; }
        public string Currency { get; set; }
        public decimal? ConversionRate { get; set; }
        public string FobCif { get; set; }
        public string AirCfa { get; set; }
        public string PrinceRange { get; set; }
        public decimal? QuatePrice { get; set; }
        public string Remarks { get; set; }
        public int? FinalQuats { get; set; }
    }
}
