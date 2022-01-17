using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewReturnRMRecv
    {
        public string Code { get; set; }
        public string Full_Name { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryName { get; set; }
        public string CName { get; set; }
        public string AddressOffice { get; set; }
        public string AddressWork { get; set; }
        public string Name { get; set; }
        public string SupplierName { get; set; }
        public string RackNo { get; set; }
        public long ReturnRMRecvID { get; set; }
        public long ReturnRMRecvSNo { get; set; }
        public string ReturnRMRecvNo { get; set; }
        public DateTime ReturnRMRecvDate { get; set; }
        public string SessionYear { get; set; }
        public int UserID { get; set; }
        public string ReturnRMNo { get; set; }
        public long ReturnRMID { get; set; }
        public long SupplierId { get; set; }
        public long ReturnRMRecvChildID { get; set; }
        public long RitemId { get; set; }
        public double ReturnQty { get; set; }
        public double ReturnSide { get; set; }
        public string Unit { get; set; }
        public int SNo { get; set; }
        public string RMRemark { get; set; }
        public long RackId { get; set; }
        public long CategoryID { get; set; }
        public long SubCategoryID { get; set; }

        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public string LotNo { get; set; }
    }
}
