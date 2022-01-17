using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewUserRights
    {
        public string MenuName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int MenuID { get; set; }
        public long UserRightsId { get; set; }
        public string MenuArea { get; set; }
        public string MenuController { get; set; }
        public string MenuAction { get; set; }
        public int MenuCategoryID { get; set; }
        public string MenuCategoryName { get; set; }
    }
}
