using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ERP.Web.Areas.Menu.Models
{
    public class MenuModel
    {
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public string MenuURL { get; set; }
        public int MenuCategoryID { get; set; }
        public long MenuCategoryName { get; set; }
        public string MenuArea { get; set; }
        public string MenuController { get; set; }
        public string MenuAction { get; set; }

        public SelectList MenuCategoryNameList { set; get; }
        public List<UserMenuCat> UserMenuCat { get; set; }
    }

    public class UserMenuCat
    {
        public int MenuCategoryID { get; set; }
        public string MenuCategoryName { get; set; }
        public List<UserMenu> UserMenu { get; set; }
    }

    public class UserMenu
    {
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public string MenuURL { get; set; }
        public int MenuCategoryID { get; set; }
        public string MenuArea { get; set; }
        public string MenuController { get; set; }
        public string MenuAction { get; set; }
    }
}