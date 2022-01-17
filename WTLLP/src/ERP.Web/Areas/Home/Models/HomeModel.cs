using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using ERP.Domain.Entities;

namespace ERP.Web.Areas.Home.Models
{
    public class HomeModel
    {
        public List<ViewDashBoardBuyerLastOrder> ViewDashBoardBuyerLastOrder { get; set; }
        public List<ViewDashBoardOrderToBeDeliver> ViewDashBoardOrderToBeDeliver { get; set; }
    }
}