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

namespace ERP.Web.Areas.Width.Controllers
{
    [AuthLogin]
    [Area("Width")]
    public class WidthController : Controller
    {
        WidthDetails _width = new WidthDetails();
        IHostingEnvironment _env;
        IWidthContract _iwidth;

        public WidthController(IHostingEnvironment env, IWidthContract width)
        {
            _env = env;
            _iwidth = width;
        }

        public ActionResult WidthList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("WidthList " + ex.Message);
            }
        }

        public ActionResult WidthList_Partial(String search)
        {
            try
            {
                return PartialView("_WidthList", _iwidth.GetWidthList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("WidthList_Partial " + ex.Message);
            }
        }

        public ActionResult CreateWidth()
        {
            try
            {
                _width = new WidthDetails();
                return View(_width);
            }
            catch (Exception ex)
            {
                return Json("CreateWidth " + ex.Message);
            }
        }

        public ActionResult DeleteWidth(int id)
        {
            try
            {
                _iwidth.Delete(id);
                return RedirectToAction("WidthList");
            }
            catch (Exception ex)
            {
                return Json("DeleteWidth " + ex.Message);
            }
        }

        public ActionResult EditWidth(int id)
        {
            try
            {
                _width = new WidthDetails();
                _width = _iwidth.Get(id).Result;
                return View(_width);
            }
            catch (Exception ex)
            {
                return Json("EditWidth " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateWidth(WidthDetails width)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    //duplicate = _iwidth.CheckDuplicate(width.WidthName).Result;
                    if (duplicate == 0)
                    {
                        width.EntryDate = DateTime.Now.Date;
                        width.SessionYear = Helper.Create_Session();
                        width.UserId = HttpContext.Session.GetInt32("userid").Value;
                        width.WidthId = _iwidth.GetNewWidthId().Result;
                        string result = _iwidth.Post(width).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(width);
            }
            catch (Exception ex)
            {
                return Json("CreateWidth " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditWidth")]
        public IActionResult EditWidth(WidthDetails width)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string result = _iwidth.Put(width).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(width);
            }
            catch (Exception ex)
            {
                return Json("EditWidth " + ex.Message);
            }
        }
    }
}
