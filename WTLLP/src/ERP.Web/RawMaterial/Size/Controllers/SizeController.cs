using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ERP.Web.Areas.Size.Controllers
{
    [AuthLogin]
    [Area("Size")]
    public class SizeController : Controller
    {
        SizeDetails _size = new SizeDetails();
        IHostingEnvironment _env;
        ISizeContract _isize;

        public SizeController(IHostingEnvironment env, ISizeContract sz)
        {
            _env = env;
            _isize = sz;
        }

        public ActionResult CreateSize()
        {
            try
            {
                _size = new SizeDetails();
                return View(_size);
            }
            catch (Exception ex)
            {
                return Json("CreateSize " + ex.Message);
            }
        }

        public ActionResult SizeList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("SizeList " + ex.Message);
            }
        }

        public ActionResult SizeList_Partial(String search)
        {
            try
            {
                return PartialView("_SizeList", _isize.GetSizeList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("SizeList_Partial " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateSize(SizeDetails SZ)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _isize.CheckDuplicate(SZ.SizeName).Result;
                    if (duplicate == 0)
                    {
                        SZ.EntryDate = DateTime.Now.Date;
                        SZ.SessionYear = Helper.Create_Session();
                        SZ.UserId = HttpContext.Session.GetInt32("userid").Value;
                        SZ.SizeId = _isize.GetNewSizeId().Result;
                        string result = _isize.Post(SZ).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(SZ);
            }
            catch (Exception ex)
            {
                return Json("CreateSize " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditSize")]
        public IActionResult EditSize(SizeDetails SZ)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string result = _isize.Put(SZ).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(SZ);
            }
            catch (Exception ex)
            {
                return Json("EditSize " + ex.Message);
            }
        }

        public ActionResult EditSize(int id)
        {
            try
            {
                _size = new SizeDetails();
                _size = _isize.Get(id).Result;
                return View(_size);
            }
            catch (Exception ex)
            {
                return Json("EditSize " + ex.Message);
            }
        }

        public ActionResult DeleteSize(int id)
        {
            try
            {
                _isize.Delete(id);
                return RedirectToAction("SizeList");
            }
            catch (Exception ex)
            {
                return Json("DeleteSize " + ex.Message);
            }
        }
    }
}
