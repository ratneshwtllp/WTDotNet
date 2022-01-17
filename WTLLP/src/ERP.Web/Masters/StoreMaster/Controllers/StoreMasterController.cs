using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using ERP.Web.Areas.StoreMaster.Models;
using Microsoft.AspNetCore.Http;

namespace ERP.Web.Areas.StoreMaster.Controllers
{
    [AuthLogin]
    [Area("StoreMaster")]
    public class StoreMasterController : Controller
    {
        StoreMasterModel _storemodel = new StoreMasterModel();
        IHostingEnvironment _env;
        IStoreMasterContract _istore;

        public StoreMasterController(IHostingEnvironment env, IStoreMasterContract stm)
        {
            _env = env;
            _istore = stm;
        }
        
        public ActionResult StoreMasterList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("StoreMasterList " + ex.Message);
            }
        }

        public ActionResult StoreMasterList_Partial(String search)
        {
            try
            {
                return PartialView("_StoreMasterList", _istore.GetStoreMasterList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("StoreMasterList_Partial " + ex.Message);
            }
        }

        public ActionResult CreateStoreMaster()
        {
            try
            {
                _storemodel = new StoreMasterModel();
                _storemodel.StoreId = _istore.GetNewStoreMasterId().Result;
                return View(_storemodel);
            }
            catch (Exception ex)
            {
                return Json("CreateStoreMaster " + ex.Message);
            }
        }

        public ActionResult EditStoreMaster(int id)
        {
            try
            {
                StoreMasterDetails _store = new StoreMasterDetails();
                _store = _istore.Get(id).Result;
                _storemodel = new StoreMasterModel();
                _storemodel.StoreId = _store.StoreId;
                _storemodel.StoreName = _store.StoreName;
                return View(_storemodel);
            }
            catch (Exception ex)
            {
                return Json("EditStoreMaster " + ex.Message);
            }
        }

        public ActionResult DeleteStoreMaster(int id)
        {
            try
            {
                _istore.Delete(id);
                return RedirectToAction("StoreMasterList");
            }
            catch (Exception ex)
            {
                return Json("DeleteStoreMaster " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateStoreMaster(StoreMasterModel SMM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    if (duplicate == 0)
                    {
                        StoreMasterDetails _store = new StoreMasterDetails();
                        _store.StoreId = _istore.GetNewStoreMasterId().Result;
                        _store.StoreName = SMM.StoreName;
                        _store.EntryDate = DateTime.Now.Date;
                        _store.SessionYear = Helper.Create_Session();
                        _store.UserId = HttpContext.Session.GetInt32("userid").Value;
                        string result = _istore.Post(_store).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(SMM);
            }
            catch (Exception ex)
            {
                return Json("CreateStoreMaster " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditStoreMaster")]
        public IActionResult EditStoreMaster(StoreMasterModel smm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    StoreMasterDetails _store = new StoreMasterDetails();
                    _store.StoreId = smm.StoreId;
                    _store.StoreName = smm.StoreName;
                    _store.EntryDate = DateTime.Now.Date;
                    _store.SessionYear = Helper.Create_Session();
                    _store.UserId = HttpContext.Session.GetInt32("userid").Value;
                    string result = _istore.Put(_store).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(smm);
            }
            catch (Exception ex)
            {
                return Json("EditStoreMaster " + ex.Message);
            }
        }
    }
}
