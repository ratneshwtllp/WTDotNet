using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewRTSGRN
    {
        public long RTSGRNID { get; set; }
        public DateTime RTSGRNDate { get; set; }
        public long RTSGRNSNo { get; set; }
        public string RTSGRNNo { get; set; }
        public string GRNNO { get; set; }
        public DateTime GRNDate { get; set; }
        public string BillNo { get; set; }
        public string ChallanNo { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime ChallanDate { get; set; }
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public long GRNID { get; set; }
        public long RTSGRNChildID { get; set; }
        public long GRNChildID { get; set; }
        public long POChildID { get; set; }
        public long POID { get; set; }
        public string PONO { get; set; }
        public DateTime PODate { get; set; }
        public long RItem_Id { get; set; }
        public string Full_Name { get; set; }
        public double RTSQtySuppUnit { get; set; }
        public double RTSQty { get; set; }
        public int RTSSide { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public string HSNCode { get; set; }
        public int TaxId { get; set; }
        public string TaxName { get; set; }

        public double IGSTPer { get; set; }
        public double IGSTAmt { get; set; }
        public double CGSTPer { get; set; }
        public double CGSTAmt { get; set; }
        public double SGSTPer { get; set; }
        public double SGSTAmt { get; set; }
        public double AmtAfterTax { get; set; }
        public string RTSGRNRemark { get; set; }
        public double TotalQtySUnit { get; set; }
        public double TotalQty { get; set; }
        public int TotalSide { get; set; }
        public double TotalAmount { get; set; }
        public double TotalCGSTAmt { get; set; }
        public double TotalSGSTAmt { get; set; }
        public double TotalIGSTAmt { get; set; }
        public double TotalTaxAmt { get; set; }
        public double TotalAmtAfterTax { get; set; }

        public double GRNQty { get; set; }
        public string LotNo { get; set; }
        public double GRNQtySuppUnit { get; set; }
        public int GRNQtySide { get; set; }
        public string Unit { get; set; }
        public string UName { get; set; }

        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
