using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class BuyerCartonStickerTemplate
    {
        public int BuyerCartonStickerTemplateID { get; set; }
        public long BuyerId { get; set; }
        public int TemplateID { get; set; }
    }
}
