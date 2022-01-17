using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class GRNChild
    { 
        public long GRNChildID { get; set; }
        public long GRNID { get; set; } 
        public long POChildID { get; set; }
        public double GRNQty { get; set; }
        public string LotNo { get; set; }
        public int SNo { get; set; }
        public double GRNQtySuppUnit { get; set; }
        public int GRNQtySide { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public long RItem_Id { get; set; }

        public double FUnitRate { get; set; }
        public string HSNCode { get; set; }
        public int TaxId { get; set; }
        public double IGSTPer { get; set; }
        public double IGSTAmt { get; set; }
        public double CGSTPer { get; set; }
        public double CGSTAmt { get; set; }
        public double SGSTPer { get; set; }
        public double SGSTAmt { get; set; }
        public double AmtAfterTax { get; set; }
        public int PO_JobWork { get; set; }
        public string PO_JW_Number { get; set; }

        public virtual GRNMaster GRNMaster { get; set; }
    }
}
