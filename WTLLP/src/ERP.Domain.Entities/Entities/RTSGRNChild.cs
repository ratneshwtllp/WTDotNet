using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class RTSGRNChild
    { 
        public long RTSGRNChildID { get; set; } 
        public long RTSGRNID { get; set; }
        public long GRNChildID { get; set; }
        public int SNo { get; set; }
        public long RItem_Id { get; set; }

        public double RTSQtySuppUnit { get; set; }
        public double RTSQty { get; set; }
        public int RTSSide { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }

        public string HSNCode { get; set; }
        public int TaxId { get; set; }
        public double IGSTPer { get; set; }
        public double IGSTAmt { get; set; }
        public double CGSTPer { get; set; }
        public double CGSTAmt { get; set; }
        public double SGSTPer { get; set; }
        public double SGSTAmt { get; set; }
        public double AmtAfterTax { get; set; }

        public virtual RTSGRNMaster RTSGRNMaster { get; set; }
    }
}
