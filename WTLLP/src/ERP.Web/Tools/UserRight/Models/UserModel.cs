using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.UserRight.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public SelectList UserList { set; get; }
        public int MenuCategoryID { get; set; }
        public SelectList MenuCategoryList { set; get; }
        public String MenuName { get; set; }

        public List<UserRightModel> UserRightModel { get; set; }
        public SelectList MenuList { get; set; }
    }
}