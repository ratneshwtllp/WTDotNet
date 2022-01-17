using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public class VisitorRegistorDetails
    { 
        public long VisitorRegistorId { get; set; }
        public DateTime VisitDate { get; set; }
        public string VisitorsName { get; set; }
        public string VisitorFrom { get; set; }
        public string WhomToMeet { get; set; }
        public string Purpose { get; set; }
        public string MobileNo { get; set; }
        public string InTime { get; set; }
        public string InTimeAMPM { get; set; }
        public string OutTime { get; set; }
        public string OutTimeAMPM { get; set; }
        public string RemarkIfAny { get; set; }
        public string PhotoPath { get; set; }
        public string Session_Year { get; set; }
        public int? UserId { get; set; }
    }
}
