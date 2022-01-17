using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.Contractor.Models
{
    public class ContractorModel
    {
        public long ContractorID { get; set; }
        [Display(Name = "Contractor Name")]
        public string ContractorName { get; set; }

        [Display(Name = "Company")]
        public long CompanyID { get; set; }
        [Display(Name = "Phone")]
        public string Phone1 { get; set; }
        [Display(Name = "Mobile")]
        public string Mobile1 { get; set; }
        public string Address { get; set; }
        [Display(Name = "Opening Balance")]
        public double OpeningBalance { get; set; }
        [Display(Name = "Opening Date")]
        public DateTime OpeningDate { get; set; }

        [Display(Name = "Company")]
        public int ID { get; set; }
        public string CName { get; set; }
        public SelectList CompanyList { set; get; }

        [Display(Name = "Is Payable")]
        public int IsPayable { get; set; }
        [Display(Name = "Active/Deactive")]
        public int Active_Deactive { get; set; }
        public SelectList ActiveDeactiveList { set; get; }

        public bool Selected { get; set; }
        public int UserId { get; set; }

    }
}