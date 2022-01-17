using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.Card.Models
{
    public class CardModel
    {
        [Display(Name = "Card Entry No.")]
        public int CardId { get; set; }
        [Display(Name = "Rep. Name")]
        public string RepresentetiveName { get; set; }
        [Display(Name = "Firm Name")]
        public string FirmName { get; set; }
        [Display(Name = "Address")]
        public string CardAddress { get; set; }    
        [Display(Name = "Website")]
        public string Website { get; set; }
        [Display(Name = "Email")]
        public string CardEmail { get; set; }
        [Display(Name = "Designetion")]
        public string CradDesignetion { get; set; }
        [Display(Name = "Phone1")]
        public string CardPhoneNo1 { get; set; }
        [Display(Name = "Phone2")]
        public string CardPhoneNo2 { get; set; }
        [Display(Name = "Phone3")]
        public string CardPhoneNo3 { get; set; }
        [Display(Name = "Mobile1")]
        public string CardMobileNo1 { get; set; }
        [Display(Name = "Mobile2")]
        public string CardMobileNo2 { get; set; }
        [Display(Name = "Mobile3")]
        public string CardMobileNo3 { get; set; }
        [Display(Name = "Description")]
        public string CradDescription { get; set; }
      
       
    }
}