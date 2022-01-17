using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class CartonDetails
    {
        public int CartonId { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string Dimension { get; set; }
        public string Description { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
        public int? OuterInner { get; set; }
        public int? BrandId { get; set; }
    }
}
