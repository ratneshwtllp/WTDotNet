using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Domain.Entities
{
    public partial class UploadDetails
    {
        public string MYSQLServer_Name_IP { get; set; }
        public string MYSQLServer_Uid { get; set; }
        public string MYSQLServer_Pwd { get; set; }
        public string MYSQLServer_Database { get; set; }
        public string MYSQLServer_Port { get; set; }

        public string HostURL { get; set; }
        public string FTPServerHost { get; set; }
        public string FTPUserID { get; set; }
        public string FTPPassword { get; set; }

        public string SQLServer_Name_IP { get; set; }
        public string SQLServer_Uid { get; set; }
        public string SQLServer_Pwd { get; set; }
        public string SQLServer_Database { get; set; }
    }
}
