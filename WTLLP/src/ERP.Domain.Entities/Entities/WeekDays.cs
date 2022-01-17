using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class WeekDays
    {
        public int WeekDaysId { get; set; }
        public int Year { get; set; }
        public int WeekId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NoOfDays { get; set; }
        public string Comment { get; set; }
    }
}
