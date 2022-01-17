using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewMenu
    {
        public int MenuCategoryID { get; set; }
        public string MenuCategoryName { get; set; }
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public string MenuURL { get; set; }
        public string MenuArea { get; set; }
        public string MenuController { get; set; }
        public string MenuAction { get; set; }
    }
}
