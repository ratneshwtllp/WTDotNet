using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class SalaryDetailsMaster
    {
        public long SalId { get; set; }
        public int EmpId { get; set; }
        public int? MonthId { get; set; }
        public string SYear { get; set; }
        public short? WDays { get; set; }
        public double? EmpPDay { get; set; }
        public double? Cl { get; set; }
        public double? El { get; set; }
        public double? Ltc { get; set; }
        public double? OtherLeave { get; set; }
        public double? PDays { get; set; }
        public decimal? SalRate { get; set; }
        public decimal? ActulPay { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? OtherAmt { get; set; }
        public decimal? PfRate { get; set; }
        public decimal? EsiRate { get; set; }
        public decimal? Bonus { get; set; }
        public decimal? Dadiff { get; set; }
        public float? EpfEmplyePer { get; set; }
        public decimal? EpfEmplyeAmt { get; set; }
        public float? EsiEmplyePer { get; set; }
        public decimal? EsiEmplyeAmt { get; set; }
        public float? EpfEmprPer { get; set; }
        public decimal? EpfEmprAmt { get; set; }
        public float? EsiEmprPer { get; set; }
        public decimal? EsiEmprAmt { get; set; }
        public float? ContriEmprPer { get; set; }
        public decimal? ContriEmprAmt { get; set; }
        public decimal? IncomTax { get; set; }
        public decimal? Lic { get; set; }
        public decimal? Advance { get; set; }
        public decimal? TotDeduction { get; set; }
        public decimal? TotContribut { get; set; }
        public decimal? GrdTot { get; set; }
        public decimal? NetSalary { get; set; }
        public DateTime? SalDate { get; set; }
        public string ContriType { get; set; }
        public string DeduType { get; set; }
        public decimal? EsiLimit { get; set; }
        public decimal? PfLimit { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
        public decimal? RoundOff { get; set; }
    }
}
