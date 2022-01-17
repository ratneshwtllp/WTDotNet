using ERP.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using ERP.Web.Areas.Home.Models;
using Microsoft.AspNetCore.Hosting;
using ERP.Business.Contracts;

namespace ERP.Web.Areas.Home.Controllers
{
    [AuthLogin]
    [Area("Home")]
    public class HomeController : Controller
    {
        IHostingEnvironment _env;
        IDashBoard _iDashBoard;
        HomeModel _HomeModel = new HomeModel();

        public HomeController(IHostingEnvironment env, IDashBoard dashboard)
        {
            _env = env;
            _iDashBoard = dashboard;
        }

        public IActionResult Index()
        {
            try
            {
                //SO = new SaleOrderModel();
                //SO.MonthYearIdForDashboard = DateTime.Now.Date.Month.ToString() + "_" + DateTime.Now.Date.Year.ToString();
                //SO.MonthYearListForDashboard = Helper.ConvertObjectToSelectList(_isaleorder.GetMonthYearForDashboard().Result);
                //return View(SO);
                return View();
            }
            catch (Exception ex)
            {
                return Json("Index " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Index_Partial(OrderSearch Obj)
        {
            try
            {
                _HomeModel = new HomeModel();
                _HomeModel.ViewDashBoardBuyerLastOrder =  _iDashBoard.GetViewDashBoardBuyerLastOrder(Obj).Result;
                _HomeModel.ViewDashBoardOrderToBeDeliver = _iDashBoard.GetViewDashBoardOrderToBeDeliver(Obj).Result;
                return View("_Index",_HomeModel);
            }
            catch (Exception ex)
            {
                return Json("Index " + ex.Message);
            }
        }

        public IActionResult About()
        {
            try
            {
                ViewData["Message"] = "Your application description page.";

                return View();
            }
            catch (Exception ex)
            {
                return Json("About " + ex.Message);
            }
        }

        public IActionResult Contact()
        {
            try
            {
                ViewData["Message"] = "Your contact page.";

                return View();
            }
            catch (Exception ex)
            {
                return Json("Contact " + ex.Message);
            }
        }

        public IActionResult Error()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("Error " + ex.Message);
            }
        }
    }
}
