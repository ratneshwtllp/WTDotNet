using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using ERP.Web.Areas.Consignee.Models;
using Microsoft.AspNetCore.Http;

namespace ERP.Web.Areas.Consignee.Controllers
{
    [AuthLogin]
    [Area("Consignee")]
    public class ConsigneeController : Controller
    {
        ConsigneeModel _consigneemodel = new ConsigneeModel();
        IHostingEnvironment _env;
        IConsigneeContract _iconsignee;

        public ConsigneeController(IHostingEnvironment env, IConsigneeContract consignee)
        {
            _env = env;
            _iconsignee = consignee;
        }
        
        public ActionResult ConsigneeList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("ConsigneeList " + ex.Message);
            }
        }

        public ActionResult ConsigneeList_Partial(String search)
        {
            try
            {
                return PartialView("_ConsigneeList", _iconsignee.GetConsigneeList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("ConsigneeList_Partial " + ex.Message);
            }
        }

        [HttpGet]
        public JsonResult GetStateList(int id)
        {
            try
            {
                return Json(_iconsignee.GetStateList(id).Result);
            }
            catch (Exception ex)
            {
                return Json("GetStateList " + ex.Message);
            }
        }

        [HttpGet]
        public JsonResult GetCityList(int id)
        {
            try
            {
                return Json(_iconsignee.GetCityList(id).Result);
            }
            catch (Exception ex)
            {
                return Json("GetCityList " + ex.Message);
            }
        }

        public ActionResult CreateConsignee()
        {
            try
            {
                _consigneemodel = new ConsigneeModel();
                _consigneemodel.CountryList = Helper.ConvertObjectToSelectList(_iconsignee.GetCountryList().Result);
                _consigneemodel.StateList = Helper.FillDropDownList_Empty();
                _consigneemodel.CityList = Helper.FillDropDownList_Empty();
                //_suppliermodel.StateList = Helper.ConvertObjectToSelectList(_isupplier.GetStateList().Result);
                //_suppliermodel.CityList = Helper.ConvertObjectToSelectList(_isupplier.GetCityList().Result);
                return View(_consigneemodel);
            }
            catch (Exception ex)
            {
                return Json("CreateConsignee " + ex.Message);
            }
        }

        public ActionResult EditConsignee(int id)
        {
            try
            {
                ConsigneeDetails _consignee = new ConsigneeDetails();
                _consignee = _iconsignee.Get(id).Result;

                _consigneemodel = new ConsigneeModel();
                _consigneemodel.CountryList = Helper.ConvertObjectToSelectList(_iconsignee.GetCountryList().Result);
                //_suppliermodel.StateList = Helper.ConvertObjectToSelectList(_isupplier.GetStateList().Result);
                //_suppliermodel.CityList = Helper.ConvertObjectToSelectList(_isupplier.GetCityList().Result);
                _consigneemodel.StateList = Helper.ConvertObjectToSelectList(_iconsignee.GetStateList(_consignee.CountryId).Result);
                _consigneemodel.CityList = Helper.ConvertObjectToSelectList(_iconsignee.GetCityList(_consignee.StateId).Result);

                _consigneemodel.ConsigneeId = _consignee.ConsigneeId;
                _consigneemodel.ConsigneeCode = _consignee.ConsigneeCode;
                _consigneemodel.ConsigneeName = _consignee.ConsigneeName;
                _consigneemodel.ConsigneeAddress = _consignee.ConsigneeAddress;
                _consigneemodel.CountryId = _consignee.CountryId;
                _consigneemodel.StateId = _consignee.StateId;
                _consigneemodel.CityId = _consignee.CityId;
                _consigneemodel.ConsigneePin = _consignee.ConsigneePin;
                _consigneemodel.ConsigneeEmail = _consignee.ConsigneeEmail;
                _consigneemodel.ConsigneeMobile = _consignee.ConsigneeMobile;
                _consigneemodel.ConsigneePhone = _consignee.ConsigneePhone;

                return View(_consigneemodel);
            }
            catch (Exception ex)
            {
                return Json("EditConsignee " + ex.Message);
            }
        }

        public ActionResult DeleteConsignee(int id)
        {
            try
            {
                _iconsignee.Delete(id);
                return RedirectToAction("ConsigneeList");
            }
            catch (Exception ex)
            {
                return Json("DeleteConsignee " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateConsignee(ConsigneeModel conm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _iconsignee.CheckDuplicate(conm.ConsigneeName).Result;
                    if (duplicate == 0)
                    {
                        ConsigneeDetails _consignee = new ConsigneeDetails();
                        _consignee.EntryDate = DateTime.Now.Date;
                        _consignee.SessionYear = Helper.Create_Session();
                        _consignee.UserId = HttpContext.Session.GetInt32("userid").Value;

                        _consignee.ConsigneeId = _iconsignee.GetNewConsigneeId().Result;
                        _consignee.ConsigneeCode = conm.ConsigneeCode;
                        _consignee.ConsigneeName = conm.ConsigneeName;
                        _consignee.ConsigneeAddress = conm.ConsigneeAddress;
                        _consignee.CountryId = conm.CountryId;
                        _consignee.StateId = conm.StateId;
                        _consignee.CityId = conm.CityId;
                        _consignee.ConsigneePin = conm.ConsigneePin;
                        _consignee.ConsigneeEmail = conm.ConsigneeEmail;
                        _consignee.ConsigneeMobile = conm.ConsigneeMobile;
                        _consignee.ConsigneePhone = conm.ConsigneePhone;

                        string result = _iconsignee.Post(_consignee).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(conm);
            }
            catch (Exception ex)
            {
                return Json("CreateConsignee " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditConsignee")]
        public IActionResult EditConsignee(ConsigneeModel conm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ConsigneeDetails _consignee = new ConsigneeDetails();
                    _consignee.ConsigneeId = conm.ConsigneeId;
                    _consignee.ConsigneeCode = conm.ConsigneeCode;
                    _consignee.ConsigneeName = conm.ConsigneeName;
                    _consignee.ConsigneeAddress = conm.ConsigneeAddress;
                    _consignee.CountryId = conm.CountryId;
                    _consignee.StateId = conm.StateId;
                    _consignee.CityId = conm.CityId;
                    _consignee.ConsigneePin = conm.ConsigneePin;
                    _consignee.ConsigneeEmail = conm.ConsigneeEmail;
                    _consignee.ConsigneeMobile = conm.ConsigneeMobile;
                    _consignee.ConsigneePhone = conm.ConsigneePhone;

                    string result = _iconsignee.Put(_consignee).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(conm);
            }
            catch (Exception ex)
            {
                return Json("EditConsignee " + ex.Message);
            }
        }

        public JsonResult RefreshCountry()
        {
            try
            {
                return Json(_iconsignee.GetCountryList().Result);
            }
            catch (Exception ex)
            {
                return Json("RefreshState " + ex.Message);
            }
        }
    }
}
