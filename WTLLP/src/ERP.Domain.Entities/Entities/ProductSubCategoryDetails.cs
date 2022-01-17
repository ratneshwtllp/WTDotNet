using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ProductSubCategoryDetails
    {
        public long ProductSubCategoryID { get; set; }
        public long ProductCategoryID { get; set; }
        public string ProductSubCategoryName { get; set; }
        public string Description { get; set; }
        public string SessionYear { get; set; }
        public int UserID { get; set; }
        public string ProductShortCode { get; set; }
        public long ProductStartWith { get; set; }
        public long Belongto { get; set; }
        public int CatLevel { get; set; }
        public int Online_Transfer { get; set; }
        public string SubCatImagePath { get; set; }
    }
}
