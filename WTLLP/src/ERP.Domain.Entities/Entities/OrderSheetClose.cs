using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class OrderSheetClose
    {
        public long OrderSheetCloseId { get; set; }
        public long BuyerId { get; set; }
        public long OrderMasterID { get; set; }
        public DateTime CloseDate { get; set; }
        public string CloseRemark { get; set; }
        public int UserId { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
    }
}
