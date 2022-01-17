using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class StickerDetials
    {
        public int StickerID { get; set; }
        public string StickerName { get; set; }
        public string Description { get; set; }
        public DateTime  EntryDate { get; set; }
        public string Sessionyear { get; set; }
        public int UserID { get; set; }
        public int BuyerId { get; set; }
    }
}
