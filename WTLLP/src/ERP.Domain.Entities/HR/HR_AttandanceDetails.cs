using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class HR_AttandanceDetails
    {
        public long AttandanceID { get; set; }
        public DateTime AttandanceDate { get; set; }
        public long EmployeeId { get; set; }
        public string AttandanceStatus { get; set; }
        public string AttandanceType { get; set; }
        public int INHRS { get; set; }
        public int INMIN { get; set; }
        public string INAMPM { get; set; }
        public int OUTHRS { get; set; }
        public int OUTMIN { get; set; }
        public string OUTAMPM { get; set; }
        public int Lunch { get; set; }
        public int WHRS { get; set; }
        public int WMIN { get; set; }
        public DateTime EntryDate { get; set; }
        public int UserId { get; set; }
        public string SessionYear { get; set; }
    }
}

