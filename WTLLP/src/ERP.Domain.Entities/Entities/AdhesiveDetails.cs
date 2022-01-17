using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Domain.Entities
{
    public partial class AdhesiveDetails
    {
        public int ADID { get; set; }
        public string ADName { get; set; }
        public string Description { get; set; }
        public string session_year { get; set; }
        public DateTime Date { get; set; }
        public int UserID { get; set; }
        //ADID, ADName, Description, Date, session_year, UserID
    }
}
