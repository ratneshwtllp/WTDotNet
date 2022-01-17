using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewEmployeeDetails
    {
        public long EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public int TitleId { get; set; }
        public int DesignationId { get; set; }
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
        public int RStateId { get; set; }
        public int RCityId { get; set; }
        public string RPinCode { get; set; }
        public string PAddress { get; set; }
        public int PStateId { get; set; }
        public int PCityId { get; set; }
        public string PPinCode { get; set; }
        public string PhotoPath { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public string TitleName { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string GenderName { get; set; }
        public string MaritalStatusName { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string PCityName { get; set; }
        public string PStateName { get; set; }
        public string DesignationName { get; set; }
        public int GradeId { get; set; }
        public string GradeName { get; set; }
    }
}

