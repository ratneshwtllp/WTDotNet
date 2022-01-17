using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Domain.Entities
{
    public partial class MultiLevelSubCategory
    {
        public long TableID { get; set; }
        public long ID { get; set; }
        public string Name { get; set; }
        public long Belongto { get; set; }
        public int CatLevel { get; set; }
        public long CategoryID { get; set; }
    }
}
