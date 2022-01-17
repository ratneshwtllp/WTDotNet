using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;

namespace ERP.Web.Areas.QuickTest.Controllers
{
    [AuthLogin]
    [Area("QuickTest")]
    public class QuickTestController : Controller
    {
        QuickTestDetails _test = new QuickTestDetails(); 
        IHostingEnvironment _env;
        IQuickTestContract _itest;

        public QuickTestController(IHostingEnvironment env, IQuickTestContract  un)
        {
            _env = env;
            _itest = un;
        }

        public ActionResult QuickTestList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("QuickTestList " + ex.Message);
            }
        }

        public ActionResult Quicktest_Partial(String search)
        {
            try
            {
                return PartialView("_QuickTestList", _itest.GetQuickTestList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("Quicktest_Partial " + ex.Message);
            }
        }

        public ActionResult CreateQuickTest()
        {
            try
            {
                _test = new QuickTestDetails();
                return View(_test);
            }
            catch (Exception ex)
            {
                return Json("CreateQuickTest " + ex.Message);
            }
        }

        public ActionResult DeleteQuickTest(int id)
        {
            try
            {
                _itest.Delete(id);
                return RedirectToAction("QuickTestList");
            }
            catch (Exception ex)
            {
                return Json("DeleteQuickTest " + ex.Message);
            }
        }

        public ActionResult EditQuickTest(int id)
        {
            try
            {
                _test = new QuickTestDetails();
                _test = _itest.Get(id).Result;
                return View(_test);
            }
            catch (Exception ex)
            {
                return Json("EditQuickTest " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateQuickTest(QuickTestDetails QT)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    QT.ID = _itest.GetNewQuickTestId().Result;
                    string result = _itest.Post(QT).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(QT);
            }
            catch (Exception ex)
            {
                return Json("CreateQuickTest " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditQuickTest")]
        public IActionResult EditQuickTest(QuickTestDetails QT)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string result = _itest.Put(QT).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(QT);
            }
            catch (Exception ex)
            {
                return Json("EditQuickTest " + ex.Message);
            }
        }
    }
}
