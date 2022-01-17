using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using ERP.Web.Areas.HSN.Models;

namespace ERP.Web.Areas.HSN.Controllers
{
    [AuthLogin]
    [Area("HSN")]
    public class HSNController : Controller
    {
        HSNModel _HSNmodel = new HSNModel();
        IHostingEnvironment _env;
        IHSNContract _iHSN;

        public HSNController(IHostingEnvironment env, IHSNContract HSN)
        {
            _env = env;
            _iHSN = HSN;
        }
        
        public ActionResult HSNList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("HSNList " + ex.Message);
            }
        }

        public ActionResult HSNList_Partial(String  search)
        {
            try
            {
                return PartialView("_HSNList", _iHSN.GetHSNList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("HSNList_Partial " + ex.Message);
            }
        }

        public ActionResult CreateHSN()
        {
            try
            {
                _HSNmodel = new HSNModel();
                _HSNmodel.TaxList = Helper.ConvertObjectToSelectList(_iHSN.GetTaxList().Result);
                return View(_HSNmodel);
            }
            catch (Exception ex)
            {
                return Json("CreateHSN " + ex.Message);
            }
        }

        public ActionResult EditHSN(int id)
        {
            try
            {
                ViewHSNDetails _vHSN = new ViewHSNDetails();
                _vHSN = _iHSN.Get(id).Result;

                _HSNmodel = new HSNModel();
                _HSNmodel.TaxList = Helper.ConvertObjectToSelectList(_iHSN.GetTaxList().Result);

                _HSNmodel.HSNId = _vHSN.HSNId;
                _HSNmodel.TaxId = _vHSN.TaxId;
                _HSNmodel.HSNCode = _vHSN.HSNCode;
                _HSNmodel.Remark = _vHSN.Remark;

                return View(_HSNmodel);
            }
            catch (Exception ex)
            {
                return Json("EditHSN " + ex.Message);
            }
        }

        public ActionResult DeleteHSN(int id)
        {
            try
            {
                _iHSN.Delete(id);
                return RedirectToAction("HSNList");
            }
            catch (Exception ex)
            {
                return Json("DeleteHSN " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateHSN(HSNModel HSN)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _iHSN.CheckDuplicate(HSN.HSNCode ).Result;
                    if (duplicate == 0)
                    {

                        HSNDetails _HSNMapping = new HSNDetails();
                        _HSNMapping.HSNId  = _iHSN.GetNewHSNId ().Result;
                        _HSNMapping.TaxId  = HSN.TaxId ;
                        _HSNMapping.HSNCode  = HSN.HSNCode;
                        _HSNMapping.Remark  = HSN.Remark;
                        string result = _iHSN.Post(_HSNMapping).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(HSN);
            }
            catch (Exception ex)
            {
                return Json("CreateHSN " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditHSN")]
        public IActionResult EditHSNMapping(HSNModel HSN)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HSNDetails _HSNMapping = new HSNDetails();
                    _HSNMapping.HSNId  = HSN.HSNId;
                    _HSNMapping.TaxId  = HSN.TaxId ;
                    _HSNMapping.HSNCode  = HSN.HSNCode;
                    _HSNMapping.Remark  = HSN.Remark;
                    string result = _iHSN.Put(_HSNMapping).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(HSN);
            }
            catch (Exception ex)
            {
                return Json("EditHSNMapping " + ex.Message);
            }
        }

        public JsonResult RefreshTex()
        { 
            try
            {
                return Json(_iHSN.GetTaxList().Result);
            }
            catch (Exception ex)
            {
                return Json("RefreshCountry " + ex.Message);
            }
        }

    }
}
