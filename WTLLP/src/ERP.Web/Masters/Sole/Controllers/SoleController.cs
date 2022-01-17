using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using ERP.Web.Areas.Sole.Models;
using Microsoft.AspNetCore.Http;

namespace ERP.Web.Areas.Sole.Controllers
{
    [AuthLogin]
    [Area("Sole")]
    public class SoleController : Controller
    {
        SoleModel _Solemodel = new SoleModel();
        IHostingEnvironment _env;
        ISoleContract _isole;

        public SoleController(IHostingEnvironment env, ISoleContract ISC)
        {
            _env = env;
            _isole = ISC;
        }
        
        public ActionResult SoleList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("SoleList " + ex.Message);
            }
        }

        public ActionResult SoleList_Partial(String search)
        {
            try
            {
                return PartialView("_SoleList", _isole.GetSoleList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("SoleList_Partial " + ex.Message);
            }
        }

        public ActionResult CreateSole()
        {
            try
            {
                _Solemodel = new SoleModel();
                _Solemodel.SoleId = _isole.GetNewSoleId().Result;
                return View(_Solemodel);
            }
            catch (Exception ex)
            {
                return Json("CreateSole " + ex.Message);
            }
        }

        public ActionResult EditSole(int id)
        {
            try
            {
                SoleMaster _sole = new SoleMaster();
                _sole = _isole.Get(id).Result;
                _Solemodel = new SoleModel();
                _Solemodel.SoleId = _sole.SoleId;
                _Solemodel.SoleName = _sole.SoleName;
                _Solemodel.Remark = _sole.Remark;
                return View(_Solemodel);
            }
            catch (Exception ex)
            {
                return Json("EditSole " + ex.Message);
            }
        }

        public ActionResult DeleteSole(int id)
        {
            try
            {
                _isole.Delete(id);
                return RedirectToAction("SoleList");
            }
            catch (Exception ex)
            {
                return Json("DeleteSole " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateSole(SoleModel SM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _isole.CheckDuplicate(SM.SoleName).Result;
                    if (duplicate == 0)
                    {
                        SoleMaster _sole = new SoleMaster();
                        _sole.SoleId = _isole.GetNewSoleId().Result;
                        _sole.SoleName = SM.SoleName;
                        _sole.Remark = SM.Remark;
                        _sole.SessionYear = Helper.Create_Session();
                        _sole.UserId = HttpContext.Session.GetInt32("userid").Value;
                        string result = _isole.Post(_sole).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(SM);
            }
            catch (Exception ex)
            {
                return Json("CreateSole " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditSole")]
        public IActionResult EditSole(SoleModel SM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SoleMaster _sole = new SoleMaster();
                    _sole.SoleId = SM.SoleId;
                    _sole.SoleName = SM.SoleName;
                    _sole.Remark = SM.Remark;
                    _sole.SessionYear = Helper.Create_Session();
                    _sole.UserId = HttpContext.Session.GetInt32("userid").Value;
                    string result = _isole.Put(_sole).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(SM);
            }
            catch (Exception ex)
            {
                return Json("EditSole " + ex.Message);
            }
        }
    }
}
