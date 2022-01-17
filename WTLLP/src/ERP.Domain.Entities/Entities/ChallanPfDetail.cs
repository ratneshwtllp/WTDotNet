using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ChallanPfDetail
    {
        public int ChPfid { get; set; }
        public string ChPfBankcode { get; set; }
        public string ChPfYearPf { get; set; }
        public int? ChPfMonthid { get; set; }
        public DateTime? ChPfPaydate { get; set; }
        public int? ChPfTsac1 { get; set; }
        public int? ChPfTsac10 { get; set; }
        public int? ChPfTsac21 { get; set; }
        public decimal? ChPfTwac1 { get; set; }
        public decimal? ChPfTwac10 { get; set; }
        public decimal? ChPfTwac21 { get; set; }
        public decimal? ChPfEmprcon1 { get; set; }
        public decimal? ChPfEmprcon10 { get; set; }
        public decimal? ChPfEmprcon21 { get; set; }
        public decimal? ChPfEmployec1 { get; set; }
        public decimal? ChPfAdm2 { get; set; }
        public decimal? ChPfAdm22 { get; set; }
        public decimal? ChPfInsp2 { get; set; }
        public decimal? ChPfInsp22 { get; set; }
        public decimal? ChPfPenal1 { get; set; }
        public decimal? ChPfPenal2 { get; set; }
        public decimal? ChPfPenal10 { get; set; }
        public decimal? ChPfPenal21 { get; set; }
        public decimal? ChPfPenal22 { get; set; }
        public decimal? ChPfMisc2 { get; set; }
        public decimal? ChPfMisc22 { get; set; }
        public int? Cid { get; set; }
        public string AmtInWords { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
