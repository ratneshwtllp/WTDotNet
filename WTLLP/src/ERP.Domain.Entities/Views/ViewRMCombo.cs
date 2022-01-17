using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{ 
    public class ViewRMCombo
    { 
        public string Comp_Name { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CostUnit { get; set; }
        public string CompGroupName { get; set; }
        public string SupplierName { get; set; }
        public double CWAS { get; set; }
        public double BOMWAS { get; set; }
        public double CostPrice { get; set; }
        public int Comp_Id { get; set; }
        public long RMComboChildID { get; set; }
        public long RMComboID { get; set; }
        public long RitemId { get; set; }
        public long CompID { get; set; }
        public int CompQty { get; set; }
        public int SerialNo { get; set; }
        public int CompGroupId { get; set; }
        public string Remark { get; set; }
        public string Photo { get; set; }
        public double RMQty { get; set; }
        public long SupplierId { get; set; }
        public int ProcessID { get; set; }
        public string UName { get; set; }
        public string USName { get; set; }
    }
}
