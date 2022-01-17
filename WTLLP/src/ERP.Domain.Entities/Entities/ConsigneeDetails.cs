using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ConsigneeDetails
    {
        public long ConsigneeId { get; set; }
        public string ConsigneeCode { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneeAddress { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string ConsigneePin { get; set; }
        public string ConsigneeEmail { get; set; }
        public string ConsigneeMobile { get; set; }
        public string ConsigneePhone { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int? UserId { get; set; }
    }
}
