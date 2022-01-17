using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class MenuDetails
    {
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public string MenuURL { get; set; }
        public int MenuCategoryID { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public string MenuArea { get; set; }
        public string MenuController { get; set; }
        public string MenuAction { get; set; }
    }
}
