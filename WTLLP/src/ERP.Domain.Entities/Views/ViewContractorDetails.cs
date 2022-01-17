using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewContractorDetails
    { 
        public long ContractorID { get; set; }
        public string ContractorName { get; set; }
        //public long CompanyID { get; set; }
        public string Phone1 { get; set; }
        public string Mobile1 { get; set; }
        public string Address { get; set; }
        public double OpeningBalance { get; set; }
        public DateTime OpeningDate { get; set; }

        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserID { get; set; }
        public int ID { get; set; }
        public string CName { get; set; }       //Company Name
        public int IsPayable { get; set; }
        public int Active_Deactive { get; set; }

    }
}
