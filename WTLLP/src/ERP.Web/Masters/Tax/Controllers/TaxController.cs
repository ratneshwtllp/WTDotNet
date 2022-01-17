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

namespace ERP.Web.Areas.Tax.Controllers
{
    [AuthLogin]
    [Area("Tax")]
    public class TaxController : Controller
    {
        TaxDetails _tax = new TaxDetails();
        IHostingEnvironment _env;   
        ITaxContract _itax;

        public TaxController(IHostingEnvironment env, ITaxContract tax)
        {
            _env = env;
            _itax = tax;
        }

        public ActionResult TaxList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("TaxList " + ex.Message);
            }
        }

        public ActionResult TaxList_Partial(String search)
        {
            try
            {
                return PartialView("_TaxList", _itax.GetTaxList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("TaxList_Partial " + ex.Message);
            }
        }

        public ActionResult CreateTax()
        {
            try
            {
                _tax = new TaxDetails();
                return View(_tax);
            }
            catch (Exception ex)
            {
                return Json("CreateTax " + ex.Message);
            }
        }

        public ActionResult DeleteTax(int id)
        {
            try
            {
                _itax.Delete(id);
                return RedirectToAction("TaxList");
            }
            catch (Exception ex)
            {
                return Json("DeleteTax " + ex.Message);
            }
        }

        public ActionResult EditTax(int id)
        {
            try
            {
                _tax = new TaxDetails();
                _tax = _itax.Get(id).Result;
                return View(_tax);
            }
            catch (Exception ex)
            {
                return Json("EditTax " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateTax(TaxDetails Tax)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _itax.CheckDuplicate(Tax.TaxFullName).Result;
                    if (duplicate == 0)
                    { 
                        Tax.EntryDate = DateTime.Now.Date;
                        Tax.SessionYear = Helper.Create_Session();
                        Tax.UserId = HttpContext.Session.GetInt32("userid").Value;
                        Tax.TaxId = _itax.GetNewTaxId().Result;
                        string result = _itax.Post(Tax).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }

                }
                return View(Tax);
            }
            catch (Exception ex)
            {
                return Json("CreateTax " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditTax")]
        public IActionResult EditTax(TaxDetails Tax)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string result = _itax.Put(Tax).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(Tax);
            }
            catch (Exception ex)
            {
                return Json("EditTax " + ex.Message);
            }
        }
    }
}
