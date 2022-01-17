using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using ERP.Web.Areas.Zipper.Models;
using Microsoft.AspNetCore.Http;

namespace ERP.Web.Areas.Zipper.Controllers
{
    [AuthLogin]
    [Area("Zipper")]
    public class ZipperController : Controller
    {
        ZipperModel _zippermodel = new ZipperModel();
        IHostingEnvironment _env;
        IZipperContract _izipper;

        public ZipperController(IHostingEnvironment env, IZipperContract IZC)
        {
            _env = env;
            _izipper = IZC;
        }
        
        public ActionResult ZipperList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("ZipperList " + ex.Message);
            }
        }

        public ActionResult ZipperList_Partial(String search)
        {
            try
            {
                return PartialView("_ZipperList", _izipper.GetZipperList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("ZipperList_Partial " + ex.Message);
            }
        }

        public ActionResult CreateZipper()
        {
            try
            {
                _zippermodel = new ZipperModel();
                _zippermodel.ZipperId = _izipper.GetNewZipperId().Result;
                return View(_zippermodel);
            }
            catch (Exception ex)
            {
                return Json("CreateZipper " + ex.Message);
            }
        }

        public ActionResult EditZipper(int id)
        {
            try
            {
                ZipperMaster _zipper = new ZipperMaster();
                _zipper = _izipper.Get(id).Result;
                _zippermodel = new ZipperModel();
                _zippermodel.ZipperId = _zipper.ZipperId;
                _zippermodel.ZipperName = _zipper.ZipperName;
                _zippermodel.Remark = _zipper.Remark;
                return View(_zippermodel);
            }
            catch (Exception ex)
            {
                return Json("EditZipper " + ex.Message);
            }
        }

        public ActionResult DeleteZipper(int id)
        {
            try
            {
                _izipper.Delete(id);
                return RedirectToAction("ZipperList");
            }
            catch (Exception ex)
            {
                return Json("DeleteZipper " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateZipper(ZipperModel ZM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _izipper.CheckDuplicate(ZM.ZipperName).Result;
                    if (duplicate == 0)
                    {
                        ZipperMaster _zipper = new ZipperMaster();
                        _zipper.ZipperId = _izipper.GetNewZipperId().Result;
                        _zipper.ZipperName = ZM.ZipperName;
                        _zipper.Remark = ZM.Remark;
                        _zipper.SessionYear = Helper.Create_Session();
                        _zipper.UserId = HttpContext.Session.GetInt32("userid").Value;
                        string result = _izipper.Post(_zipper).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(ZM);
            }
            catch (Exception ex)
            {
                return Json("CreateZipper " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditZipper")]
        public IActionResult EditZipper(ZipperModel ZM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ZipperMaster _zipper = new ZipperMaster();
                    _zipper.ZipperId = ZM.ZipperId;
                    _zipper.ZipperName = ZM.ZipperName;
                    _zipper.Remark = ZM.Remark;
                    _zipper.SessionYear = Helper.Create_Session();
                    _zipper.UserId = HttpContext.Session.GetInt32("userid").Value;
                    string result = _izipper.Put(_zipper).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(ZM);
            }
            catch (Exception ex)
            {
                return Json("EditZipper " + ex.Message);
            }
        }
    }
}
