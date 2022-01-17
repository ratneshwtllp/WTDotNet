using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewAdjustmentRM
    {
        public long AdjustmentRMStockID { get; set; }
        public long RitemID { get; set; }
        public long SupplierID { get; set; }
        public long RackID { get; set; }
        public double Quantity { get; set; }
        public int StockType { get; set; }
        public string Remark { get; set; }
        public string Full_Name { get; set; }
        public string SupplierName { get; set; }
        public string RackNo { get; set; }
        public string SubCategoryName { get; set; }
        public DateTime AdjustmentDate { get; set; }

        public long SubCategoryID { get; set; }
        public long CategoryID { get; set; }
        public string Code { get; set; }
        public string LotNo { get; set; }

        public int CostUnit { get; set; }
        public int UID { get; set; }
        public string USName { get; set; }

    }
}
