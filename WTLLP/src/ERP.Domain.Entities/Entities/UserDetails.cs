using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class UserDetails
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public long DepartmentId { get; set; }
        public int DepartmentHead { get; set; }
        public int UserTypeId { get; set; }
        public int IsActive { get; set; }
    }
}
