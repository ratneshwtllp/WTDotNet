using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using ERP.Web.Areas.City.Models;
using Microsoft.AspNetCore.Http;

namespace ERP.Web.Areas.City.Controllers
{
    [AuthLogin]
    [Area("City")]
    public class CityController : Controller
    {
        CityModel _citymodel = new CityModel();
        IHostingEnvironment _env;
        ICityContract _icity;

        public CityController(IHostingEnvironment env, ICityContract city)
        {
            _env = env;
            _icity = city;
        }
        
        public ActionResult CityList()
        {
            try
            {
                //if(HttpContext.Session.GetInt32("userid") == null)
                //{
                //    return RedirectToAction("Login","Login", new { Area ="Login"});
                //}
                return View();
            }
            catch (Exception ex)
            {
                return Json("CityList " + ex.Message);
            }
        }

        public ActionResult CityList_Partial(String search)
        {
            try
            {
                return PartialView("_CityList", _icity.GetCityList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("CityList_Partial " + ex.Message);
            }
        }

        [HttpGet]
        public JsonResult GetStateList(int id)
        {
            try
            {
                return Json(_icity.GetStateList(id).Result);
            }
            catch (Exception ex)
            {
                return Json("GetStateList " + ex.Message);
            }
        }

        public ActionResult CreateCity()
        {
            try
            {
                _citymodel = new CityModel();
                _citymodel.CountryList = Helper.ConvertObjectToSelectList(_icity.GetCountryList().Result);
                _citymodel.StateList = Helper.FillDropDownList_Empty();
                return View(_citymodel);
            }
            catch (Exception ex)
            {
                return Json("CreateCity " + ex.Message);
            }
        }

        public ActionResult EditCity(int id)
        {
            try
            {
                ViewCityDetails _vcity = new ViewCityDetails();
                _vcity = _icity.Get(id).Result;

                _citymodel = new CityModel();
                _citymodel.CountryList = Helper.ConvertObjectToSelectList(_icity.GetCountryList().Result);
                _citymodel.StateList = Helper.ConvertObjectToSelectList(_icity.GetStateList(_vcity.CountryId).Result);

                _citymodel.CityId = _vcity.CityId;
                _citymodel.CityName = _vcity.CityName;
                _citymodel.StateId = _vcity.StateId;
                _citymodel.CountryId = _vcity.CountryId;

                return View(_citymodel);
            }
            catch (Exception ex)
            {
                return Json("EditCity " + ex.Message);
            }
        }

        public ActionResult DeleteCity(int id)
        {
            try
            {
                _icity.Delete(id);
                return RedirectToAction("CityList");
            }
            catch (Exception ex)
            {
                return Json("DeleteCity " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateCity(CityModel citym)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _icity.CheckDuplicate(citym.CityName).Result;
                    if (duplicate == 0)
                    {
                        CityDetails _city = new CityDetails();
                        _city.EntryDate = DateTime.Now.Date;
                        _city.SessionYear = Helper.Create_Session();
                        _city.UserID = HttpContext.Session.GetInt32("userid").Value;
                        _city.CityId = _icity.GetNewCityId().Result;
                        _city.CityName = citym.CityName;
                        _city.StateId = citym.StateId;

                        string result = _icity.Post(_city).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(citym);
            }
            catch (Exception ex)
            {
                return Json("CreateCity " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditCity")]
        public IActionResult EditCity(CityModel citym)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CityDetails _city = new CityDetails();
                    _city.CityId = citym.CityId;
                    _city.CityName = citym.CityName;
                    _city.StateId = citym.StateId;
                    string result = _icity.Put(_city).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(citym);
            }
            catch (Exception ex)
            {
                return Json("EditCity " + ex.Message);
            }
        }

        public JsonResult RefreshCountry()
        {
            try
            {
                return Json(_icity.GetCountryList().Result);
            }
            catch (Exception ex)
            {
                return Json("RefreshCountry " + ex.Message);
            }
        }

    }
}
