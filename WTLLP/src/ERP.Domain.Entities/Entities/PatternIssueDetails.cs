using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public class PatternIssueDetails
    {
        public long PIID { get; set; }
        public long FItemId { get; set; }
        public string Code { get; set; }
        public DateTime IssueDate { get; set; }
        public int IssueTo { get; set; }
        public string Session_Year { get; set; }
        public int? UserId { get; set; }
        public int Work_Plan { get; set; }
        public long Plan_ID { get; set; }
        public int IssueNo { get; set; }
        public int IssueQty { get; set; }
    }
}
