using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class NonReturnableOutPassInvoiceMaster
    {
        public long NropInvId { get; set; }
        public long NropInvSno { get; set; }
        public string NropInvNo { get; set; }
        public DateTime Nropdate { get; set; }
        public int SampleExport { get; set; }
        public long SampleExportId { get; set; }
        public string SampleExportNo { get; set; }
        public int Package { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
