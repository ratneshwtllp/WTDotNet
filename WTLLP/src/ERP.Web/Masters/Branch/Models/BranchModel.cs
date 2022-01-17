using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.Branch.Models
{
    public class BranchModel
    {
        public long BranchID { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        [Display(Name = "State")]
        public int StateId { get; set; }
        [Display(Name = "City")]
        public int CityId { get; set; }
                  
        public SelectList StateList { set; get; }
         
        public SelectList CityList { set; get; }
    }
}