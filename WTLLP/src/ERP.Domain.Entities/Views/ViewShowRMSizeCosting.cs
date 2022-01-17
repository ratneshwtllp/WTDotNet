using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Domain.Entities
{
    public partial class ViewShowRMSizeCosting
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string ProductSubCategoryName { get; set; }
        public long ProductCategoryID { get; set; }
        public long ProductSubCategoryID { get; set; }
        public string Code { get; set; }
        public string PhotoPath     { get; set; }
        public string ProductCategoryName { get; set; }
        public string CLName { get; set; }
        public string ProductSizeName { get; set; }
        public int CLID { get; set; }
        public long CostingSizeRMId { get; set; }
        public long CostingID { get; set; }
        public long FitemId { get; set; }
        public int SizeId { get; set; }
        public long RItemID { get; set; }
     
        
    }
}
