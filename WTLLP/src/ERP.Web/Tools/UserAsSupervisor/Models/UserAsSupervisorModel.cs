using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using ERP.Web.Areas.Contractor.Models;
namespace ERP.Web.Areas.UserAsSupervisor.Models
{
    public partial class UserAsSupervisorModel
    {
        public long UserAsSupervisorId { get; set; }
        public int UserId { get; set; }
        public long ContractorID { get; set; }
        public string UserName { get; set; }
        public string ContractorName { get; set; }
        public SelectList ContactorList { set; get; }
        public bool Selected { get; set; }
        public List<ContractorModel> ContractorModel { get; set; }
        public SelectList UserList { set; get; }

    }
}
