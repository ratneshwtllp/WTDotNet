using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using ERP.Business.Contracts;
using ERP.Domain.Entities;

namespace ERP.Web.Tools.DashBoard.Controllers
{
    public class DashBoardController : Controller
    {

        IHostingEnvironment _env;
        IDashBoard _iDashBoard;

        public DashBoardController(IHostingEnvironment env, IDashBoard dashboard)
        {
            _env = env;
            _iDashBoard = dashboard;
        }

        public ActionResult GetViewDashBoardBuyerLastOrder(OrderSearch Obj)
        {
            List<ViewDashBoardBuyerLastOrder> _List = _iDashBoard.GetViewDashBoardBuyerLastOrder(Obj).Result;
            return View("BuyerLastOrder");
        }

        public ActionResult GetViewDashBoardOrderToBeDeliver(OrderSearch Obj)
        {
            List<ViewDashBoardOrderToBeDeliver> _List = _iDashBoard.GetViewDashBoardOrderToBeDeliver(Obj).Result;
            return View("OrderToBeDeliver");
        }
    }
}
