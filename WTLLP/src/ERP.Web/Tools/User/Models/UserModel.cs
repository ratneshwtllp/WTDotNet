using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.User.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Login Name")]
        public string LoginName { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }

        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }

        [Display(Name = "Department")]
        public long DepartmentId { get; set; }

        [Display(Name = "Admin")]
        public int DepartmentHead { get; set; }

        public bool Selected { get; set; }

        public SelectList DepartmentList { set; get; }
        [Display(Name = "User Type")]
        public int UserTypeId { get; set; }
        public SelectList UserTypeList { set; get; }

        public bool ChkIsActive { get; set; }  
        public int IsActive { get; set; }
    }
}