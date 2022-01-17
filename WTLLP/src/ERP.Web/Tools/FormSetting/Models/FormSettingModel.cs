using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.FormSetting.Models
{
    public class FormSettingModel
    {
        public int SNo { set; get; }
        public string FormName { set; get; }
        public string Prefix { set; get; }
        public string SessionYear { set; get; }
        public int StartingNumber { set; get; }
        public int NoOfDigits { set; get; }
        public string DispalyNumber { set; get; }
        public DateTime EntryDate { get; set; }
        public int IsBatchOfRM { set; get; }
    }
}