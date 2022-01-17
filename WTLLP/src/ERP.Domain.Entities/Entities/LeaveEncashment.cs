using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class LeaveEncashment
    {
        public int LeaveId { get; set; }
        public int? LEmpId { get; set; }
        public int? LDepttId { get; set; }
        public int? LDesigId { get; set; }
        public decimal? LAmount { get; set; }
        public double? LEmpPfPer { get; set; }
        public decimal? LEmpPf { get; set; }
        public double? LEmpEsiPer { get; set; }
        public decimal? LEmpEsi { get; set; }
        public decimal? LEmpDeduction { get; set; }
        public short? LMonthid { get; set; }
        public string LYear { get; set; }
        public double? LEmployerPfPer { get; set; }
        public decimal? LEmployerPf { get; set; }
        public double? LEmployerEsiPer { get; set; }
        public decimal? LEmployerEsi { get; set; }
        public double? LEmployerContriPer { get; set; }
        public decimal? LEmployerContri { get; set; }
        public string LForYear { get; set; }
        public decimal? LPayableamt { get; set; }
        public int? LSalId { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
