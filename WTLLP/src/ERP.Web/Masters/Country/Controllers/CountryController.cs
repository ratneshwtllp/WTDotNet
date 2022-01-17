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

namespace ERP.Web.Areas.Trimming.Controllers
{
    [AuthLogin]
    [Area("Country")]
    public class CountryController : Controller
    {
        CountryDetails _country = new CountryDetails();
        IHostingEnvironment _env;   
        ICountryContract _icountry;

        public CountryController(IHostingEnvironment env, ICountryContract country)
        {
            _env = env;
            _icountry = country;
        }

        public ActionResult CountryList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("CountryList " + ex.Message);
            }
        }

        public ActionResult CountryList_Partial(String search)
        {
            try
            {
                return PartialView("_CountryList", _icountry.GetCountryList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("CountryList_Partial " + ex.Message);
            }
        }

        public ActionResult CreateCountry()
        {
            try
            {
                _country = new CountryDetails();
                return View(_country);
            }
            catch (Exception ex)
            {
                return Json("CreateCountry " + ex.Message);
            }
        }

        public ActionResult DeleteCountry(int id)
        {
            try
            {
                _icountry.Delete(id);
                return RedirectToAction("CountryList");
            }
            catch (Exception ex)
            {
                return Json("DeleteCountry " + ex.Message);
            }
        }

        public ActionResult EditCountry(int id)
        {
            try
            {
                _country = new CountryDetails();
                _country = _icountry.Get(id).Result;
                return View(_country);
            }
            catch (Exception ex)
            {
                return Json("EditCountry " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateCountry(CountryDetails Country)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _icountry.CheckDuplicate(Country.CountryName).Result;
                    if (duplicate == 0)
                    {
                        Country.EntryDate = DateTime.Now.Date;
                        Country.SessionYear = Helper.Create_Session();
                        Country.UserID = HttpContext.Session.GetInt32("userid").Value;
                        Country.CountryId = _icountry.GetNewCountryId().Result;
                        string result = _icountry.Post(Country).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }

                }
                return View(Country);
            }
            catch (Exception ex)
            {
                return Json("CreateCountry " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditCountry")]
        public IActionResult EditCountry(CountryDetails Country)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string result = _icountry.Put(Country).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(Country);
            }
            catch (Exception ex)
            {
                return Json("EditCountry " + ex.Message);
            }
        }
    }
}
