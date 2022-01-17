using System;
using System.Collections.Generic;

namespace ERP.Web.Areas.UserRight.Models
{
    public partial class UserRightModel
    {
        public long UserRightsId { get; set; }
        public int UserId { get; set; }
        public int MenuId { get; set; }
        public String MenuName { get; set; }
        public bool Selected { get; set; }
    }
}
