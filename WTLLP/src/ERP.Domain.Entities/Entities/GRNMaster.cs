using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class GRNMaster
    { 
        public long GRNID { get; set; }
        public long SupplierID { get; set; }
        public long POID { get; set; }
        public string GRNNO { get; set; }
        public DateTime GRNDate { get; set; }
        public string BillNo { get; set; }
        public string ChallanNo { get; set; }
        public string VehicleNo { get; set; }
        public string DriverName { get; set; }
        public string Remark { get; set; }
        public long GRNSerial { get; set; }
        public double TotalQty { get; set; }
        public double TotalAmount { get; set; }
        public int StoreID { get; set; }
        public int POManual { get; set; }
        public int Verify { get; set; }             //store keeping status
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserID { get; set; }
        public int CancelStatus { get; set; }
        public double TotalQtySUnit { get; set; }
        public int TotalQtySide { get; set; }
        public double FreightAmount { get; set; }
        public double OtherAmoumt { get; set; }
        public double GrandTotal { get; set; }
        public string ReferenceNumber { get; set; }
        public long BranchId { get; set; }
        public string  ShopDetails { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime ChallanDate { get; set; }

        public double TotalCGSTAmt { get; set; }
        public double TotalSGSTAmt { get; set; }
        public double TotalIGSTAmt { get; set; }
        public double TotalTaxAmt { get; set; }
        public double TotalAmtAfterTax { get; set; }
        public int PO_JobWork { get; set; }
        public virtual ICollection<GRNChild> GRNChild { get; set; }
        public virtual ICollection<GRNJWRMComsumption> GRNJWRMComsumption { get; set; }
    }
}
