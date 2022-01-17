using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Domain.Entities
{
    public partial class ViewCostingSizeRM
    {
        public long CostingSizeRMId { get; set; }
        public long FitemId { get; set; }
        public int SizeId { get; set; }
        public long CostingID { get; set; }
        public long RItemID { get; set; }
        public double RMQty { get; set; }
        public int SerialNo { get; set; }
        public int IsComponent { get; set; }
        public long SupplierId { get; set; }
        public string Remark { get; set; }

        public string Full_Name { get; set; }
        public string Code { get; set; }
        public string USName { get; set; }
        public string SupplierName { get; set; }
        public string ProductSizeName { get; set; }

        public long CompID { get; set; }
        public int CompQty { get; set; }
        public int CompGroupId { get; set; }
        public string Comp_Name { get; set; }
        public string CompGroupName { get; set; }

        public int ProcessID { get; set; }

        public double CWAS { get; set; }
        public double CAfterWAS { get; set; }

        public double RMCAmount { get; set; }
        public double BOMWAS { get; set; }
        public double BOMAfterWas { get; set; }
        public double RMBAmount { get; set; }
        public long SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public string Name { set; get; }

        public double RMPrice { get; set; }
        public double CurrentPrice { get; set; }
    }
}
