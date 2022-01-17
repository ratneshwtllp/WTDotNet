using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class Fitting
    {
        public int Ftid { get; set; }
        public string FittingName { get; set; }
        public string FittingDesc { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
