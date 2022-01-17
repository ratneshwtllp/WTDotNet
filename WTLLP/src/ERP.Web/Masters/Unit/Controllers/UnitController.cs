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

namespace ERP.Web.Areas.Unit.Controllers
{
    [AuthLogin]
    [Area("Unit")]
    public class UnitController : Controller
    {
        UnitDetails _unit = new UnitDetails();
        IHostingEnvironment _env;
        IUnitContract _iunit;

        public UnitController(IHostingEnvironment env, IUnitContract un)
        {
            _env = env;
            _iunit = un;
        }

        public ActionResult UnitList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("UnitList " + ex.Message);
            }
        }

        public ActionResult UnitList_Partial(String search)
        {
            try
            {
                return PartialView("_UnitList", _iunit.GetUnitList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("UnitList_Partial " + ex.Message);
            }
        }

        public ActionResult CreateUnit()
        {
            try
            {
                _unit = new UnitDetails();
                return View(_unit);
            }
            catch (Exception ex)
            {
                return Json("CreateUnit " + ex.Message);
            }
        }

        public ActionResult DeleteUnit(int id)
        {
            try
            {
                _iunit.Delete(id);
                return RedirectToAction("UnitList");
            }
            catch (Exception ex)
            {
                return Json("DeleteUnit " + ex.Message);
            }
        }

        public ActionResult EditUnit(int id)
        {
            try
            {
                _unit = new UnitDetails();
                _unit = _iunit.Get(id).Result;
                return View(_unit);
            }
            catch (Exception ex)
            {
                return Json("EditUnit " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateUnit(UnitDetails UN)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _iunit.CheckDuplicate(UN.Uname).Result;
                    if (duplicate == 0)
                    {
                        UN.EntryDate = DateTime.Now.Date;
                        UN.SessionYear = Helper.Create_Session();
                        UN.UserId = HttpContext.Session.GetInt32("userid").Value;
                        UN.Uid = _iunit.GetNewUnitId().Result;
                        string result = _iunit.Post(UN).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(UN);
            }
            catch (Exception ex)
            {
                return Json("CreateUnit " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditUnit")]
        public IActionResult EditUnit(UnitDetails UN)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string result = _iunit.Put(UN).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(UN);
            }
            catch (Exception ex)
            {
                return Json("EditUnit " + ex.Message);
            }
        }
    }
}
