using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using ERP.Web.Areas.Bank.Models;
using Microsoft.AspNetCore.Http;

namespace ERP.Web.Areas.Bank.Controllers
{
    [AuthLogin]
    [Area("Bank")]
    public class BankController : Controller
    {
        BankModel _Bankmodel = new BankModel();
        IHostingEnvironment _env;
        IBankContract _ibank;

        public BankController(IHostingEnvironment env, IBankContract IBC)
        {
            _env = env;
            _ibank = IBC;
        }

        public ActionResult BankList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("BankList " + ex.Message);
            }
        }

        public ActionResult BankList_Partial(String search)
        {
            try
            {
                return PartialView("_BankList", _ibank.GetBankList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("BankList_Partial " + ex.Message);
            }
        }

        public ActionResult CreateBank()
        {
            try
            {
                _Bankmodel = new BankModel();
                _Bankmodel.BankId = _ibank.GetNewBankId().Result;
                return View(_Bankmodel);
            }
            catch (Exception ex)
            {
                return Json("CreateBank " + ex.Message);
            }
        }

        public ActionResult EditBank(int id)
        {
            try
            {
                BankDetails _bank = new BankDetails();
                _bank = _ibank.Get(id).Result;
                _Bankmodel = new BankModel();
                _Bankmodel.BankId = _bank.BankId;
                _Bankmodel.BankName = _bank.BankName;
                return View(_Bankmodel);
            }
            catch (Exception ex)
            {
                return Json("EditBank " + ex.Message);
            }
        }

        public ActionResult DeleteBank(int id)
        {
            try
            {
                _ibank.Delete(id);
                return RedirectToAction("BankList");
            }
            catch (Exception ex)
            {
                return Json("DeleteBank " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateBank(BankModel BM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    if (duplicate == 0)
                    {
                        BankDetails _bank = new BankDetails();
                        _bank.BankId = _ibank.GetNewBankId().Result;
                        _bank.BankName = BM.BankName;
                        _bank.SessionYear = Helper.Create_Session();
                        _bank.UserId = HttpContext.Session.GetInt32("userid").Value;
                        string result = _ibank.Post(_bank).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(BM);
            }
            catch (Exception ex)
            {
                return Json("CreateBank " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditBank")]
        public IActionResult EditBank(BankModel BM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BankDetails _bank = new BankDetails();
                    _bank.BankId = BM.BankId;
                    _bank.BankName = BM.BankName;
                    _bank.SessionYear = Helper.Create_Session();
                    _bank.UserId = HttpContext.Session.GetInt32("userid").Value;
                    string result = _ibank.Put(_bank).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(BM);
            }
            catch (Exception ex)
            {
                return Json("EditBank " + ex.Message);
            }
        }
    }
}
