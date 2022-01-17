using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public class SampleRoomDetails
    {
        public long SampleRoomId { get; set; }
        public long FItemId { get; set; }
        public int StoreQty { get; set; }
        public DateTime StoreDate { get; set; }
        public int RackNo { get; set; }
        //public DateTime? EntryDate { get; set; }
        public string Session_Year { get; set; }
        public int? UserId { get; set; }
        public int SampleTypeId { get; set; }
    }
}
