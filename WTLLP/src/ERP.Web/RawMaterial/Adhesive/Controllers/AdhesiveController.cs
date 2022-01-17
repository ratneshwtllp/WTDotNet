using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ERP.Web.Areas.Adhesive.Controllers
{
    [AuthLogin]
    [Area("Adhesive")]
    public class AdhesiveController : Controller
    {
        AdhesiveDetails _adhesive = new AdhesiveDetails();
        IHostingEnvironment _env;
        IAdhesiveContract _iadhesive;

        public AdhesiveController(IHostingEnvironment env, IAdhesiveContract iadhesive)
        {
            _env = env;
            _iadhesive = iadhesive;
        }

        public ActionResult CreateAdhesive()
        {
            try
            {
                _adhesive = new AdhesiveDetails();
                return View(_adhesive);
            }
            catch (Exception ex)
            {
                return Json("CreateAdhesive " + ex.Message);
            }
        }

        public ActionResult AdhesiveList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("AdhesiveList " + ex.Message);
            }
        }

        public ActionResult AdhesiveList_Partial(String search)
        {
            try
            {
                return PartialView("_AdhesiveList", _iadhesive.GetAdhesiveList(search).Result);

            }
            catch (Exception ex)
            {
                return Json("AdhesiveList_Partial " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateAdhesive(AdhesiveDetails ad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _iadhesive.CheckDuplicate(ad.ADName).Result;
                    if (duplicate == 0)
                    {
                        ad.session_year = Helper.Create_Session();
                        ad.UserID = HttpContext.Session.GetInt32("userid").Value;
                        ad.ADID = _iadhesive.GetNewAdhesiveId().Result;
                        ad.Date = DateTime.Now.Date;
                        string result = _iadhesive.Post(ad).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {                        
                        return BadRequest("");
                        //return BadRequest("1");
                    }
                }
                return View(ad);
            }
            catch (Exception ex)
            {
                return Json("CreateAdhesive " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditAdhesive")]
        public IActionResult EditAdhesive(AdhesiveDetails ad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string result = _iadhesive.Put(ad).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(ad);
            }
            catch (Exception ex)
            {
                return Json("EditAdhesive " + ex.Message);
            }
        }

        public ActionResult EditAdhesive(int id)
        {
            try
            {
                _adhesive = new AdhesiveDetails();
                _adhesive = _iadhesive.Get(id).Result;
                return View(_adhesive);
            }
            catch (Exception ex)
            {
                return Json("EditAdhesive " + ex.Message);
            }
        }

        public ActionResult DeleteAdhesive(int id)
        {
            try
            {
                _iadhesive.Delete(id);
                return RedirectToAction("AdhesiveList");
            }
            catch (Exception ex)
            {
                return Json("DeleteAdhesive " + ex.Message);
            }
        }
    }
}
