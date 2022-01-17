using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ERP.Web.Areas.Thickness.Controllers
{
    [AuthLogin]
    [Area("Thickness")]
    public class ThicknessController : Controller
    {
        ThicknessDetails _thick = new ThicknessDetails();
        IHostingEnvironment _env;
        IThicknessContract _ithick;

        public ThicknessController(IHostingEnvironment env, IThicknessContract th)
        {
            _env = env;
            _ithick = th;
        }

        public ActionResult CreateThickness()
        {
            try
            {
                _thick = new ThicknessDetails();
                return View(_thick);
            }
            catch (Exception ex)
            {
                return Json("CreateThickness " + ex.Message);
            }
        }

        public ActionResult ThicknessList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("ThicknessList " + ex.Message);
            }
        }

        public ActionResult ThicknessList_Partial(String search)
        {
            try
            {
                return PartialView("_ThicknessList", _ithick.GetThicknessList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("ThicknessList_Partial " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateThickness(ThicknessDetails Thick)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _ithick.CheckDuplicate(Thick.Thname).Result;
                    if (duplicate == 0)
                    {
                        Thick.EntryDate = DateTime.Now.Date;
                        Thick.SessionYear = Helper.Create_Session();
                        Thick.UserId = HttpContext.Session.GetInt32("userid").Value;
                        Thick.Thid = _ithick.GetNewThicknessId().Result;
                        string result = _ithick.Post(Thick).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(Thick);
            }
            catch (Exception ex)
            {
                return Json("CreateThickness " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditThickness")]
        public IActionResult EditThickness(ThicknessDetails Thick)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string result = _ithick.Put(Thick).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(Thick);
            }
            catch (Exception ex)
            {
                return Json("EditThickness " + ex.Message);
            }
        }

        public ActionResult EditThickness(int id)
        {
            try
            {
                _thick = new ThicknessDetails();
                _thick = _ithick.Get(id).Result;
                return View(_thick);
            }
            catch (Exception ex)
            {
                return Json("EditThickness " + ex.Message);
            }
        }

        public ActionResult DeleteThickness(int id)
        {
            try
            {
                _ithick.Delete(id);
                return RedirectToAction("ThicknessList");
            }
            catch (Exception ex)
            {
                return Json("DeleteThickness " + ex.Message);
            }
        }
    }
}
