using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class HR_HolidayMaster
    {
        public int HolidayId { get; set; }
        public string HolidayName { get; set; }
        public DateTime HolidayDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
