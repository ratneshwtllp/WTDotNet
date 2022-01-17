using System;

namespace ERP.Domain.Entities
{
    public class ViewGRNForStoreKeeping
    {
        public long GRNID { get; set; }
        public string GRNNO { get; set; }    
        public long GRNChildID { get; set; } 
        public string PONO { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long RItem_Id { get; set; }
        public double GRNQty { get; set; }
        public string LotNo { get; set; }
        public double GRNQtySuppUnit { get; set; }
        public int GRNQtySide { get; set; }
        public string CostUnit { get; set; }        
        public string Unit { get; set; }

        public double Rate { get; set; }
        public double Amount { set; get; }
        public string HSNCode { get; set; }
        public int TaxId { get; set; }
        public double IGSTPer { get; set; }
        public double IGSTAmt { get; set; }
        public double CGSTPer { get; set; }
        public double CGSTAmt { get; set; }
        public double SGSTPer { get; set; }
        public double SGSTAmt { get; set; }
        public double ConversionFactor { get; set; }
        public string TaxFullName { get; set; }
        public double AmtAfterTax { get; set; }

        public double SKQty { get; set; }
        public double SKQtySide { get; set; }
        public double RTSGRNQty { get; set; }
        public double RTSGRNQtySide { get; set; }
        public double BalQty { get; set; }
        public double BalQtySide { get; set; }

    }
}
