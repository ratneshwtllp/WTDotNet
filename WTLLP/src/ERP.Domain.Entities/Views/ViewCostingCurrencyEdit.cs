using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewCostingCurrencyEdit
    {
        //costingcurrencydetails
        public long CostingCurrencyId { get; set; }
        public long CostingId { get; set; }
        public int CID { get; set; }
        public double CRate { get; set; }
        public string FOBCIF { get; set; }
        public string FreightBy { get; set; }
        public double CAmount { get; set; }
        public double HighCAmount { get; set; }
        public double MediumCAmount { get; set; }
        public double LowCAmount { get; set; }
        public double BAmount { get; set; }
        public double HighBAmount { get; set; }
        public double MediumBAmount { get; set; }
        public double LowBAmount { get; set; }
        public int SerialNo { get; set; }
        //public string SessionYear { get; set; }
        //public int UserId { get; set; }

        //currency
        public string CName { get; set; }
        public string CSName { get; set; }
    }
}
