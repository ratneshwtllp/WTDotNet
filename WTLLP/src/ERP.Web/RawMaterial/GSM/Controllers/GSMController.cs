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

namespace ERP.Web.Areas.GSM.Controllers
{
    [AuthLogin]
    [Area("GSM")]
    public class GSMController : Controller
    {
        GSMDetails _gsm = new GSMDetails();
        IHostingEnvironment _env;
        IGSMContract _igsm;

        public GSMController(IHostingEnvironment env, IGSMContract gsm)
        {
            _env = env;
            _igsm = gsm;
        }

        public ActionResult GSMList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("GSMList " + ex.Message);
            }
        }

        public ActionResult GSMList_Partial(String search)
        {
            try
            {
                return PartialView("_GSMList", _igsm.GetGSMList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("GSMList_Partial " + ex.Message);
            }
        }

        public ActionResult CreateGSM()
        {
            try
            {
                _gsm = new GSMDetails();
                return View(_gsm);
            }
            catch (Exception ex)
            {
                return Json("CreateGSM " + ex.Message);
            }
        }

        public ActionResult DeleteGSM(int id)
        {
            try
            {
                _igsm.Delete(id);
                return RedirectToAction("GSMList");
            }
            catch (Exception ex)
            {
                return Json("DeleteGSM " + ex.Message);
            }
        }

        public ActionResult EditGSM(int id)
        {
            try
            {
                _gsm = new GSMDetails();
                _gsm = _igsm.Get(id).Result;
                return View(_gsm);
            }
            catch (Exception ex)
            {
                return Json("EditGSM " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateGSM(GSMDetails gsm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _igsm.CheckDuplicate(gsm.GSMName).Result;
                    if (duplicate == 0)
                    {
                        gsm.EntryDate = DateTime.Now.Date;
                        gsm.SessionYear = Helper.Create_Session();
                        gsm.UserId = HttpContext.Session.GetInt32("userid").Value;
                        gsm.GSMId = _igsm.GetNewGSMId().Result;
                        string result = _igsm.Post(gsm).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(gsm);
            }
            catch (Exception ex)
            {
                return Json("CreateGSM " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditGSM")]
        public IActionResult EditGSM(GSMDetails gsm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string result = _igsm.Put(gsm).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(gsm);
            }
            catch (Exception ex)
            {
                return Json("EditGSM " + ex.Message);
            }
        }
    }
}
