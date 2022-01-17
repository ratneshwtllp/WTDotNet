using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewReqRMList
    {
        public long ReqRMChildID { get; set; }
        public long ReqRMID { get; set; }
        public long RitemId { get; set; }
        public double ReqQty { get; set; }
        public double ReqSide { get; set; }
        public string Unit { get; set; }
        public int SNo { get; set; }
        public string RMRemark { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Full_Name { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryName { get; set; }
        public string Title { get; set; }
        public double CurrentStock { get; set; }
        public double IssueQty { get; set; }
    }
}
