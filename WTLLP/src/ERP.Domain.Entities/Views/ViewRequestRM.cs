using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewRequestRM
    {
        public long ReqRMID { get; set; }
        public long ReqRMSNo { get; set; }
        public string ReqRMNo { get; set; }
        public DateTime ReqRMDate { get; set; }
        public int ReqRMFor { get; set; }
        public string ReqRMRemark { get; set; }
        public int ReqRMStatus { get; set; }
        public string SessionYear { get; set; }
        public int UserID { get; set; }

        public long ReqRMChildID { get; set; }
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

        public string CName { get; set; }
        public string AddressOffice { get; set; }
        public string AddressWork { get; set; }
        public string RMFor { get; set; }
        public long ReqRMIssueID { get; set; }
        public string RequestStatus { get; set; }
        public double IssueQty { get; set; }
        public long CategoryID { get; set; }
        public long SubCategoryID { get; set; }

        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
    }
}
