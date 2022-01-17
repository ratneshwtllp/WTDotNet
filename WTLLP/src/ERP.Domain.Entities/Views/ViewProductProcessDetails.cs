using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewProductProcessDetails
    {
        //product
        public string Code { get; set; }
        public string Name { get; set; }

        //product category   
        public long ProductCategoryID { get; set; }
        public string ProductCategoryName { get; set; }

        //product sub category         
        public long ProductSubCategoryID { get; set; }
        public string ProductSubCategoryName { get; set; }

        //production process 
        public string ProcessName { get; set; } 
         
        //product process
        public long ProductProcessID { get; set; }
        public long FitemId { get; set; }
        public long ProcessID { get; set; }
        public int SNo { get; set; }
        public DateTime EntryDate { get; set; }
        public int UserId { get; set; }

        public string PhotoPath { get; set; }

        public int IsChargeable { get; set; }
        public int Charge_on_Item_Component { get; set; }
    }
}
