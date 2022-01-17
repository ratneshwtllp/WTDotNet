using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class CompanyDetails
    {
        public int Id { get; set; }
        public string CName { get; set; }
        public string AddressOffice { get; set; }
        public string AddressWork { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public string CSTT { get; set; }
        public string UPTT { get; set; }
        public string TIN { get; set; }
        public string IECode { get; set; }
        public string EmpEsiCode1 { get; set; }
        public string EmpEsiCode2 { get; set; }
        public string EmpEsiCode3 { get; set; }
        public string EmpPfEsttCode { get; set; }
        public double? Excise { get; set; }
        public string CustomTeriffNo { get; set; }
        public string PANNo { get; set; }
        public string POFooter1 { get; set; }
        public string POFooter2 { get; set; }
        public string GSTN { get; set; }

        public string LogoPath { get; set; }
    }
}
