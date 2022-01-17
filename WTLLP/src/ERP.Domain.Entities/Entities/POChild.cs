using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class POChild
    {       
        public long POChildID { get; set; }
        public long POID { get; set; }
        public int SNo { get; set; }
        public int RItem_Id { get; set; }
        public double Qty { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
        public string RDesc { get; set; }
         
        public string HSNCode { get; set; }
        public int TaxId { get; set; }
        public double IGSTPer { get; set; }
        public double IGSTAmt { get; set; }
        public double CGSTPer { get; set; }
        public double CGSTAmt { get; set; }
        public double SGSTPer { get; set; }
        public double SGSTAmt { get; set; }
        public double AmtAfterTax { get; set; } 
        public double FactoryQty { set; get; } 
        public string FactoryUnit { set; get; }
        public double ConversionFactor { get; set; }

        //public double PCSWeight { get; set; }
        //public double ActualPurPrice { get; set; }

        public virtual POMaster POMaster { get; set; }


    }
}
