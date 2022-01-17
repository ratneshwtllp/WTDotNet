using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using ERP.Web.Areas.RMBrand.Models;
using Microsoft.AspNetCore.Http;

namespace ERP.Web.Areas.RMBrand.Controllers
{
    [AuthLogin]
    [Area("RMBrand")]
    public class RMBrandController : Controller
    {
        RMBrandModel _rmbrandmodel;
        IHostingEnvironment _env;
        IRMBrandContract _irmbrand;

        public RMBrandController(IHostingEnvironment env, IRMBrandContract rmbrand)
        {
            _env = env;
            _irmbrand = rmbrand;
        }

        public ActionResult RMBrandList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("RMBrandList " + ex.Message);
            }
        }

        public ActionResult RMBrandList_Partial(String search)
        {
            try
            {
                return PartialView("_RMBrandList", _irmbrand.GetRMBrandList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("RMBrandList_Partial " + ex.Message);
            }
        }

        public ActionResult CreateRMBrand()
        {
            try
            {
                _rmbrandmodel = new RMBrandModel();
                //_rmbrandmodel.BuyerList = Helper.ConvertObjectToSelectList(_irmbrand.GetBuyerList().Result);
                return View(_rmbrandmodel);
            }
            catch (Exception ex)
            {
                return Json("CreateRMBrand " + ex.Message);
            }
        }

        public ActionResult EditRMBrand(int id)
        {
            try
            {
                RMBrandDetails _rmbrand = new RMBrandDetails();
                _rmbrand = _irmbrand.Get(id).Result;

                _rmbrandmodel = new RMBrandModel();
                //_rmbrandmodel.BuyerList = Helper.ConvertObjectToSelectList(_irmbrand.GetBuyerList().Result);

                _rmbrandmodel.RMBrandId = _rmbrand.RMBrandId;
                _rmbrandmodel.RMBrandName = _rmbrand.RMBrandName;
                _rmbrandmodel.RMBrandCode = _rmbrand.RMBrandCode;
                _rmbrandmodel.RMBrandDesc = _rmbrand.RMBrandDesc;
                //_rmbrandmodel.BuyerId = _brand.BuyerId;

                return View(_rmbrandmodel);
            }
            catch (Exception ex)
            {
                return Json("EditRMBrand " + ex.Message);
            }
        }
         
        [HttpPost]
        public ActionResult CreateRMBrand(RMBrandModel brandm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //int duplicate = 0;
                    //duplicate = _irmbrand.CheckDuplicate(brandm.BrandName).Result;
                    //if (duplicate == 0)
                    //{
                    RMBrandDetails _rmbrand = new RMBrandDetails();
                    _rmbrand.EntryDate = DateTime.Now.Date;
                    _rmbrand.SessionYear = Helper.Create_Session();
                    _rmbrand.UserId = HttpContext.Session.GetInt32("userid").Value;
                    _rmbrand.RMBrandId = _irmbrand.GetNewRMBrandId().Result;
                    _rmbrand.RMBrandName = brandm.RMBrandName;
                    _rmbrand.RMBrandCode = brandm.RMBrandCode;
                    _rmbrand.RMBrandDesc = brandm.RMBrandDesc;
                    //_rmbrand.BuyerId = brandm.BuyerId;

                    string result = _irmbrand.Post(_rmbrand).Result;
                    ModelState.Clear();
                    return Ok(result);
                    //}
                    //else
                    //{
                    //    return BadRequest("1");
                    //}
                }
                return View(brandm);
            }
            catch (Exception ex)
            {
                return Json("CreateRMBrand " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditRMBrand")]
        public IActionResult EditRMBrand(RMBrandModel rmbrandm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RMBrandDetails _rmbrand = new RMBrandDetails();
                    _rmbrand.RMBrandId = rmbrandm.RMBrandId;
                    _rmbrand.RMBrandName = rmbrandm.RMBrandName;
                    _rmbrand.RMBrandCode = rmbrandm.RMBrandCode;
                    _rmbrand.RMBrandDesc = rmbrandm.RMBrandDesc;
                    //_rmbrand.BuyerId = brandm.BuyerId;

                    string result = _irmbrand.Put(_rmbrand).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(rmbrandm);
            }
            catch (Exception ex)
            {
                return Json("EditRMBrand " + ex.Message);
            }
        }

        public ActionResult DeleteRMBrand(int id)
        {
            try
            {
                _irmbrand.Delete(id);
                return RedirectToAction("RMBrandList");
            }
            catch (Exception ex)
            {
                return Json("DeleteRMBrand " + ex.Message);
            }
        }

    }
}
