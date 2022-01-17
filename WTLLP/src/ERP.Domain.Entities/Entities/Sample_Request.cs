using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public class Sample_Request
    {
        public long Request_Id { get; set; }
        public long FItem_Id { get; set; }
        public string ItemCode { get; set; }
        public DateTime RequestDate { get; set; }
        public string ChangeMessage { get; set; }
        public string MessageFrom { get; set; }
        public DateTime ChageUpTo { get; set; }
        public int Status { get; set; }
        public string session_year { get; set; }
        public int? UserID { get; set; }
        public int SMS_Bulk { get; set; }
        public DateTime Change_On { get; set; }
        public DateTime Change_Read_On { get; set; }
    }
}
