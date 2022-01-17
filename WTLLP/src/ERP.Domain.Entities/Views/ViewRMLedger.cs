using System;
namespace ERP.Domain.Entities
{
    public partial class ViewRMLedger
    {
        public string RecordName { get; set; }
        public int RecordType { get; set; }
        public string OperationName { get; set; }
        public int OperationType { get; set; }
        public long RecordMasterId { get; set; }
        public DateTime RecordDate { get; set; }
        public string RecordNo { get; set; }
        public int UserID { get; set; }
        public long RecordChildId { get; set; }
        public long RitemId { get; set; }
        public long SupplierId { get; set; }
        public long RackId { get; set; }
        public string LotNo { get; set; }
        public double INQty { get; set; }
        public double OUTQty { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public double Stock { get; set; }
        public int UID { get; set; }
        public string USName { get; set; }
    }
}
