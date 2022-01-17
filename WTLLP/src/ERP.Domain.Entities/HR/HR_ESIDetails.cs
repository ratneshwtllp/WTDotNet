using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class HR_ESIDetails
    {
        public long ESIID { get; set; }
        public double ESILimt { get; set; }
        public double ESIEmployer { get; set; }
        public double ESIEmployee { get; set; }
        public DateTime ApplyDate { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
    }
}
