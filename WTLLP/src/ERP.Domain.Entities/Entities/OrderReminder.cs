using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class OrderReminder
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public string ReminderFor { get; set; }
        public int Afterdays { get; set; }
        public DateTime OrderDate { get; set; }
        public int Status { get; set; }
        public DateTime ReminderDate { get; set; }
        public DateTime ReceiveDate { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public string Remark { get; set; }
    }
}
