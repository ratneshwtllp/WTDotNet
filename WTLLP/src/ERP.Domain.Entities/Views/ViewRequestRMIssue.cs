using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewRequestRMIssue
    {
        public long ReqRMID { get; set; }
        public DateTime ReqRMDate { get; set; }
        public int ReqRMFor { get; set; }
        public string Code { get; set; }
        public string Full_Name { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryName { get; set; }
        public string CName { get; set; }
        public string AddressOffice { get; set; }
        public string AddressWork { get; set; }
        public string Name { get; set; }
        public long ReqRMIssueID { get; set; }
        public long ReqRMIssueSNo { get; set; }
        public string ReqRMIssueNo { get; set; }
        public DateTime ReqRMIssueDate { get; set; }
        public string ReqRMNo { get; set; }
        public long RitemId { get; set; }
        public long ReqRMIssueChildID { get; set; }
        public double IssueQty { get; set; }
        public double IssueSide { get; set; }
        public string Unit { get; set; }
        public int SNo { get; set; }
        public string RMRemark { get; set; }
        public long SupplierId { get; set; }
        public long RackId { get; set; }
        public string SupplierName { get; set; }
        public string RackNo { get; set; }
        public string RMFor { get; set; }

        public long CategoryID { get; set; }
        public long SubCategoryID { get; set; }

        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public string LotNo { get; set; }
    }
}
