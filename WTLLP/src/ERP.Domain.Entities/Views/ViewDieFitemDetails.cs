using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewDieFitemDetails
    {
        //diedetails
        public int DieId { get; set; }
        public string DieName { get; set; }
        public string DieNo { get; set; }
        public string DieDesc { get; set; }
        public long DieFitemId { get; set; }
        public long FitemId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string BuyerCode { get; set; }
        public string PartyCode { get; set; }
    }
}
