using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ProductCategoryDetails
    {
        public long ProductCategoryID { get; set; }
        public string ProductCategoryName { get; set; }
        public string ProductShortCode { get; set; }
        public long ProductStartWith { get; set; }
        public string Description { get; set; }
        public string SessionYear { get; set; }
        public int UserID { get; set; }
        public long BuyerId { get; set; }
        public string BackgroundImagePath { get; set; }
        public string CategoryImagePath { get; set; }
        public string PDFCatelogPath { get; set; }
        public int Position { get; set; }
        public int PDFCatelogStatus { get; set; }
        public int Active { get; set; }
        public int ShowInMenu { get; set; }
        public int Online_Transfer { get; set; }
    }
}
