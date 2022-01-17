using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.Buyer.Models
{
    public class BuyerConsigneeModel
    {
        public long BuyerConsigneeId { get; set; }
        public long BuyerId { get; set; }
        [Display(Name = "Consignee")]
        public long ConsigneeId { get; set; }
        public bool Selected { get; set; }
        public string  ConsigneeName { get; set; }

    }
}
