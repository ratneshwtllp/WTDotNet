using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class CareLabelDetails
    {
        public int CareLabelID { get; set; }
        public string CareLabelLName { get; set; }
        public string Description { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserID { get; set; }
        public int BuyerId { get; set; }
        public string PhotoPath { get; set; }


    }
}
