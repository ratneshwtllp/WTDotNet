using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewSticker
    {
        public long BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; }
        public int StickerID { get; set; }
        public string StickerName { get; set; }
        public string Description { get; set; }
    }
}
