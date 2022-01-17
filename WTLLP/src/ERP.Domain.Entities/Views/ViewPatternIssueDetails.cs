using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewPatternIssueDetails
    {   
        public long PIID { get; set; }
        public long FItemId { get; set; }
        public long ProductSubCategoryID { get; set; }
        public long ProductCategoryID { get; set; }
        public string ProductCategoryName { get; set; }
        public string ProductSubCategoryName { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int IssueQty { get; set; }
        public DateTime IssueDate { get; set; }
        public int IssueTo { get; set; }
        public string ContractorName { get; set; }
        public string Session_Year { get; set; }
        public int UserId { get; set; }
        public int Work_Plan { get; set; }
        public long Plan_ID { get; set; }
        public int IssueNo { get; set; }
       public int RackNo { get; set; }
    }
}
