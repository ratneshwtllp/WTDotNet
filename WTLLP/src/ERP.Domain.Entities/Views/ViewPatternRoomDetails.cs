using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewPatternRoomDetails
    {   
        public long PatternRoomId { get; set; }
        public long FItemId { get; set; }
        public int StoreQty { get; set; }
        public DateTime StoreDate { get; set; }
        public int RackNo { get; set; }
        public string Session_Year { get; set; }
        public int UserId { get; set; }
        public int ComponentQty { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long ProductSubCategoryID { get; set; }
        public long ProductCategoryID { get; set; }
        public string ProductCategoryName { get; set; }
        public string ProductSubCategoryName { get; set; }

    }
}
