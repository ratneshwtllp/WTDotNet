    using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ShapeDetails
    {
        public int ShapeId { get; set; }
        public string ShapeName { get; set; }
        public string ShapeDesc { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
