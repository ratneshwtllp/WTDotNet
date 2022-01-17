using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ClothDetails
    {
        public int Chid { get; set; }
        public string Chname { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
