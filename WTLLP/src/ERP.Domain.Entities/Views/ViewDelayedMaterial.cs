using System;

namespace ERP.Domain.Entities
{
    public class ViewDelayedMaterial
    {
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
        public long POID { get; set; }
        public string PONO { get; set; }
        public DateTime Podate { get; set; }
        public DateTime DLDate { get; set; }
        public long Expr1 { get; set; }
        public int SNo { get; set; }
       // public long RItem_Id { get; set; }
        public double Qty { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long GRNID { get; set; }
        public string GRNNO { get; set; }
        public DateTime GRNDate { get; set; }
        public string BillNo { get; set; }
        public string ChallanNo { get; set; }
        public int DelayDay { get; set; }
        public long SID { get; set; }
    }
}
