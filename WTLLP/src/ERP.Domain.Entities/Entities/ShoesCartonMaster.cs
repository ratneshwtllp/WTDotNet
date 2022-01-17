using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ShoesCartonMaster
    {  
        public long ShoesCartonMasterId { get; set; }
        public long BuyerId { get; set; }
        public long OrderId { get; set; }
        public DateTime ShoesCartonDate { get; set; }
        public long CartonNo { get; set; } 
        public string BuyerCartonNo { get; set; }
        public string ExporterCode { get; set; }
        public int CartonId { get; set; }
        public string Dimension { get; set; }
        public int TotalQty { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
          
        public virtual ICollection<ShoesCartonChild> ShoesCartonChild { get; set; }   

    }
}
