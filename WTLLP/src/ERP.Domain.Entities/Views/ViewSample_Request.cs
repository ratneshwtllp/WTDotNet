using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewSample_Request
    {
        public long Request_Id { get; set; }
        public long FItem_Id { get; set; }
        public DateTime RequestDate { get; set; }
        public string ChangeMessage { get; set; }
        public string MessageFrom { get; set; }
        public DateTime ChageUpTo { get; set; }
        public int Status { get; set; }
        public string session_year { get; set; }
        public int UserID { get; set; }
        public int SMS_Bulk { get; set; }
        public DateTime Change_On { get; set; }
        public DateTime Change_Read_On { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long ProductSubCategoryID { get; set; }
        public string ProductSubCategoryName { get; set; }
        public long ProductCategoryID { get; set; }
        public string ProductCategoryName { get; set; }
        public string PhotoPath { get; set; }
        
    }
}
