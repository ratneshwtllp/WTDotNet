using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class PackingWeightDetails
    { 
        public long PackingWeightID { get; set; }
        public long PackingID { get; set; }
        public int CartonID { get; set; }
        public int CartonNo { get; set; } 
        public string Dimension { get; set; } 
        public double CartonWeight { get; set; }
        public double GrossWeight { get; set; }
        public double NetWeight { get; set; } 
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserID { get; set; } 

        public virtual PackingMaster PackingMaster { get; set; }
    }
}
