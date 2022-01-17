using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public class StoreMasterDetails
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
