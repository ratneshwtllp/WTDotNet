using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class TempTable
    {
        public int EmpId { get; set; }
        public int EmpCode { get; set; }
        public string EmpName { get; set; }
        public string PfNo { get; set; }
        public string EmpFname { get; set; }
        public string Designation { get; set; }
        public int PDays { get; set; }
        public string Cl { get; set; }
        public string El { get; set; }
        public string Ltc { get; set; }
        public string OtherLeave { get; set; }
        public decimal? Basic { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Hra { get; set; }
        public decimal? Convey { get; set; }
        public decimal? Trp { get; set; }
        public decimal? Special { get; set; }
        public decimal? Medical { get; set; }
        public decimal? GraTotal { get; set; }
        public decimal? Pf { get; set; }
        public decimal? Esi { get; set; }
        public decimal? Advance { get; set; }
        public decimal? Suspence { get; set; }
        public decimal? RoundOff { get; set; }
        public decimal? Tds { get; set; }
        public decimal NetSalary { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Dept { get; set; }
        public string Cname { get; set; }
        public string AddressOffice { get; set; }
    }
}
