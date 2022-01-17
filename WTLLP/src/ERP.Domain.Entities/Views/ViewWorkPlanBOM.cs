using System;

namespace ERP.Domain.Entities
{
    public class ViewWorkPlanBOM
    {  
        //WorkPlanChild
        public long PlanChildId { get; set; }
        public long PlanId { get; set; }
        public long FitemId { get; set; }
        public int PlanQty { get; set; }

        //CostingRM
        public long CostingRMID { get; set; }
        public long CostingID { get; set; }
        public double CAfterWAS { get; set; }

        //Ritem
        public long RItemID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Full_Name { get; set; }
        public int CostUnit { get; set; }
        public string Title { get; set; }

        //Unit 
        public string CostUnitName { get; set; }
        public string CostUnitSName { get; set; }

        //RM Category
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string ShortCode { get; set; }

        //RM Sub Category
        public long SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }

        public double Required { get; set; }
        public double RMStock { get; set; }

        //IssueChild
        public double IssueQty { get; set; }
        public double ReserveQty { get; set; }

        public int ProductSizeId { get; set; }
        public string ProductSizeName { get; set; }
        public string ProductCode { get; set; }
    }
}
