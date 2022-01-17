using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewMoveRMStock
    {
        public long MoveRMStockID { get; set; }
        public long RitemID_From { get; set; }
        public long SupplierID_From { get; set; }
        public long RackID_From { get; set; }
        public long RitemID_To { get; set; }
        public long SupplierID_To { get; set; }
        public long RackID_To { get; set; }
        public double Quantity { get; set; }
        public string Remark { get; set; }
        public string RawMaterial_From { get; set; }
        public string RawMaterial_To { get; set; }
        public string SupplierName_From { get; set; }
        public string SubCategoryName_From { get; set; }
        public string SubCategoryName_To { get; set; }
        public string SupplierName_To { get; set; }
        public string RackNo_From { get; set; }
        public string RackNo_To { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public long SubCategoryID_From { get; set; }
        public long CategoryID_From { get; set; }
        public string CategoryName_From { get; set; }
        public string LotNo { get; set; }
    }
}
