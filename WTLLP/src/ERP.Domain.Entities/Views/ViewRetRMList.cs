using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewRetRMList
    {
        public long ReturnRMChildID { get; set; }
        public long ReturnRMID { get; set; }
        public long RitemId { get; set; }
        public double ReturnQty { get; set; }
        public double ReturnSide { get; set; }
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
        public long DepartmentId { get; set; } 
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
    }
}
