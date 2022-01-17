using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class TempTableDetails
    {
        public long TableId { get; set; }
        public long AnyId { get; set; } 
        public string FieldName { get; set; }
        public string TableName { get; set; } 
        
    }
}
