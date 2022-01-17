using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ChallanEsiDetails
    {
        public int ChEsiId { get; set; }
        public int? Cid { get; set; }
        public DateTime? ChEsiDateday { get; set; }
        public string ChEsiPaymode { get; set; }
        public string ChEsiDdno { get; set; }
        public DateTime? ChEsiDatedday { get; set; }
        public string ChEsiBankname { get; set; }
        public int? ChEsiMonthid { get; set; }
        public int? ChEsiYear { get; set; }
        public string ChEsiPaydetail { get; set; }
        public int? ChEsiNoofEmp { get; set; }
        public decimal? ChEsiTotwages { get; set; }
        public decimal? ChEsiEmpContri { get; set; }
        public decimal? ChEsiEmployerContri { get; set; }
        public decimal? ChEsiInterest { get; set; }
        public decimal? ChEsiDamages { get; set; }
        public decimal? ChEsiOthers { get; set; }
        public decimal? ChEsiTotal { get; set; }
        public string ChEsiBankDrawn { get; set; }
        public string ChEsiBranch { get; set; }
        public string AmtInWords { get; set; }
        public string DepositBank { get; set; }
        public string DepositBranch { get; set; }
        public DateTime? DepositDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
