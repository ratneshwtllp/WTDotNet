using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ERP.Web.Areas.Quality.Controllers
{
    [AuthLogin]
    [Area("Quality")]
    public class QualityController : Controller
    {
        QualityDetails _quality = new QualityDetails();
        IHostingEnvironment _env;
        IQalityContract _iquality;

        public QualityController(IHostingEnvironment env, IQalityContract ql)
        {
            _env = env;
            _iquality = ql;
        }

        public ActionResult CreateQuality()
        {
            try
            {
                _quality = new QualityDetails();
                return View(_quality);
            }
            catch (Exception ex)
            {
                return Json("CreateQuality " + ex.Message);
            }
        }

        public ActionResult QualityList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("QualityList " + ex.Message);
            }
        }

        public ActionResult QualityList_Partial(String search)
        {
            try
            {
                return PartialView("_QualityList", _iquality.GetQualityList(search).Result);

            }
            catch (Exception ex)
            {
                return Json("QualityList_Partial " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateQuality(QualityDetails QL)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _iquality.CheckDuplicate(QL.QualityName).Result;
                    if (duplicate == 0)
                    {
                        QL.EntryDate = DateTime.Now.Date;
                        QL.Sessionyear = Helper.Create_Session();
                        QL.UserID = HttpContext.Session.GetInt32("userid").Value;
                        QL.QualityId = _iquality.GetNewQualityId().Result;
                        string result = _iquality.Post(QL).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(QL);
            }
            catch (Exception ex)
            {
                return Json("CreateQuality " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditQuality")]
        public IActionResult EditQuality(QualityDetails QL)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string result = _iquality.Put(QL).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(QL);
            }
            catch (Exception ex)
            {
                return Json("EditQuality " + ex.Message);
            }
        }

        public ActionResult EditQuality(int id)
        {
            try
            {
                _quality = new QualityDetails();
                _quality = _iquality.Get(id).Result;
                return View(_quality);
            }
            catch (Exception ex)
            {
                return Json("EditQuality " + ex.Message);
            }
        }

        public ActionResult DeleteQuality(int id)
        {
            try
            {
                _iquality.Delete(id);
                return RedirectToAction("QualityList");
            }
            catch (Exception ex)
            {
                return Json("DeleteQuality " + ex.Message);
            }
        }
    }
}
