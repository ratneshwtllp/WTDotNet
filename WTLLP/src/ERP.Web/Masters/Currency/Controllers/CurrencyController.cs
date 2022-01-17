using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ERP.Web.Areas.Currency.Controllers
{
    [AuthLogin]
    [Area("Currency")]
    public class CurrencyController : Controller
    {
        CurrencyDetails _currency = new CurrencyDetails();
        IHostingEnvironment _env;
        ICurrencyContract _icurrency;

        public CurrencyController(IHostingEnvironment env, ICurrencyContract curr)
        {
            _env = env;
            _icurrency = curr;
        }

        public ActionResult CurrencyList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("CurrencyList " + ex.Message);
            }
        }

        public ActionResult CurrencyHistoryList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("CurrencyHistoryList " + ex.Message);
            }
        }

        public ActionResult CurrencyList_Partial(string search)
        {
            try
            {
                return PartialView("_CurrencyList", _icurrency.GetCurrencyList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("CurrencyList_Partial " + ex.Message);
            }
        }

        public ActionResult CurrencyHistoryList_Partial(int id)
        {
            try
            {
                return PartialView("_CurrencyHistoryList", _icurrency.GetCurrencyHistoryList(id).Result);
            }
            catch (Exception ex)
            {
                return Json("CurrencyHistoryList_Partial " + ex.Message);
            }
        }

        public ActionResult CreateCurrency()
        {
            try
            {
                _currency = new CurrencyDetails();
                return View(_currency);
            }
            catch (Exception ex)
            {
                return Json("CreateCurrency " + ex.Message);
            }
        }

        public ActionResult EditCurrency(int id)
        {
            try
            {
                _currency = new CurrencyDetails();
                _currency = _icurrency.Get(id).Result;
                return View(_currency);
            }
            catch (Exception ex)
            {
                return Json("EditCurrency " + ex.Message);
            }
        }

        public ActionResult DeleteCurrency(int id)
        {
            try
            {
                _icurrency.Delete(id);
                return RedirectToAction("CurrencyList");
            }
            catch (Exception ex)
            {
                return Json("DeleteCurrency " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateCurrency(CurrencyDetails CURR)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _icurrency.CheckDuplicate(CURR.Cname).Result;
                    if (duplicate == 0)
                    {
                        CURR.EntryDate = DateTime.Now.Date;
                        CURR.SessionYear = Helper.Create_Session();
                        CURR.UserId = HttpContext.Session.GetInt32("userid").Value;
                        CURR.Cid = _icurrency.GetNewCurrencyId().Result;
                        string result = _icurrency.Post(CURR).Result;
                        ModelState.Clear();
                        return Ok("Record Save");
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(CURR);
            }
            catch (Exception ex)
            {
                return Json("CreateCurrency " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditCurrency")]
        public IActionResult EditCurrency(CurrencyDetails CURR)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CurrencyHistoryDetails Curr_His = new CurrencyHistoryDetails();

                    List<CurrencyHistoryDetails> icollection = new List<CurrencyHistoryDetails>();
                    Curr_His.CurrencyHistoryId  = _icurrency.GetNewCurrencyHistoryId().Result;
                    Curr_His.PreviousPrice  = _icurrency.GetPreviuosPrice (CURR.Cid ).Result;
                    Curr_His.ChangeDate = DateTime.Now.Date;
                    Curr_His.CID = CURR.Cid;
                   // Curr_His.PreviousPrice = 0;
                    Curr_His.ChangePrice = CURR.Crate;
                    Curr_His.UserId = HttpContext.Session.GetInt32("userid").Value;
                    lock (icollection)
                    {
                        icollection.Add(Curr_His); 
                    }
                    CURR.CurrencyHistoryDetails = icollection;

                    string result = _icurrency.Put(CURR).Result;
                    ModelState.Clear();
                    return Ok("Record Edit.");
                }
                return View(CURR);
            }
            catch (Exception ex)
            {
                return Json("EditCurrency " + ex.Message);
            }
        }
    }
}
