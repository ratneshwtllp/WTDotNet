using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class RMMovement
    {
        public long RitemID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Full_Name { get; set; }
        public int IsTransactionToday { get; set; }
        public double OPStock_today { get; set; }
        public double CurrentStock { get; set; }
        public double StoreKeepingStock_today { get; set; }
        public double IssueQty_today { get; set; }
        public double MoveInStock_today { get; set; }
        public double MoveOutStock_today { get; set; }
        public double DamageStock_today { get; set; }
        public double IncreaseStock_today { get; set; }
        public double DecreaseStock_today { get; set; }
        public double JWSendQty_today { get; set; }
        public double JWRecvQty_today { get; set; }
        public double WPReturnQty_today { get; set; }
        public double RTSQty_today { get; set; }
        public double RequestIssueQty_today { get; set; }
        public double ReturnRecvQty_today { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string CostUnitName { get; set; }
        public DateTime OPDate { get; set; }
    }
}
