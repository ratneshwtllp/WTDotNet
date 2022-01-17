using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.Emails.Models
{
    public class EmailsModel
    {
        public long EmailSettingsID { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Email Password")]
        public string EmailPassword { get; set; }

        [Display(Name = "Email Subject")]
        public string EmailSubject { get; set; }

        [Display(Name = "Email Header")]
        public string EmailHeader { get; set; }

        [Display(Name = "Email Footer")]
        public string EmailFooter { get; set; }

        [Display(Name = "Out Going Server")]
        public string OutGoingServer { get; set; }
        public int Port { get; set; }
        [Display(Name = "Email BCC")]
        public string BCC { get; set; }
        [Display(Name = "Email TO")]
        public string EmailTO { get; set; }

        [Display(Name = "Email CC")]
        public string CC { get; set; }

        //EmailType Table

        [Display(Name = "Email Type")]
        public int EmailTypeId { get; set; }
        public string EmailTypeName { get; set; }

        public bool EditMode { get; set; }

        public SelectList EmailTypeList { get; set; }



    }
}