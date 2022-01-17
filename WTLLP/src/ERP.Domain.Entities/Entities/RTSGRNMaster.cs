using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class RTSGRNMaster
    {
        public long RTSGRNID { get; set; }
        public DateTime RTSGRNDate { get; set; }
        public long RTSGRNSNo { get; set; }
        public string RTSGRNNo { get; set; }
        public long GRNID { get; set; }
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

        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserID { get; set; }

        public virtual ICollection<RTSGRNChild> RTSGRNChild { get; set; }
    }
}
