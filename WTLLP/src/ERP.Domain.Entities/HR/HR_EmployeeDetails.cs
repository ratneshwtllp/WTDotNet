using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class HR_EmployeeDetails
    {
        public long EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public int TitleId { get; set; }
        public string EmployeeName { get; set; }
        public int DepartmentID { get; set; }
        public int DesignationId { get; set; }
        public int GradeId { get; set; }
        public DateTime DateOfJoin { get; set; }
        public string EmployeeACNo { get; set; }
        public string EmployeePFNo { get; set; }
        public string EmployeeESINo { get; set; }
        public string Dispensary { get; set; }
        public string FatherHusband { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int GenderId { get; set; }
        public int MaritalStatusId { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string PANNo { get; set; }
        public string RAddress { get; set; }
        public string PAddress { get; set; }
        public string PhotoPath { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
        public string AdharNo { get; set; }
        public string VoteridNo { get; set; }
        public int BankId { get; set; }
        public string IFSCcode { get; set; }
        public int EmployeeTypeId { get; set; }
        public string BankAcNo { get; set; }
    }
}
