using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;

namespace ERP.Web.Areas.RMCat.Controllers
{
    [AuthLogin]
    [Area("RMCat")]
    public class RMCatController : Controller
    {
        RMCategory _rmcat = new RMCategory();
        IHostingEnvironment _env;
        IRMCategoryContract _irmcat;

        public RMCatController(IHostingEnvironment env, IRMCategoryContract rmc)
        {
            _env = env;
            _irmcat = rmc;
        }

        public ActionResult CreateRMCategory()
        {
            try
            {
                _rmcat = new RMCategory();
                return View(_rmcat);
            }
            catch (Exception ex)
            {
                return Json("CreateRMCategory " + ex.Message);
            }
        }

        public ActionResult RMCategoryList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("RMCategoryList " + ex.Message);
            }
        }

        public ActionResult RMCategoryList_Partial(String search)
        {
            try
            {
                return PartialView("_RMCategoryList", _irmcat.GetRMCategoryList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("RMCategoryList_Partial " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateRMCategory(RMCategory rmc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _irmcat.CheckDuplicate(rmc.CategoryName).Result;
                    if (duplicate == 0)
                    {
                        rmc.ShortCode = "-";
                        rmc.CategoryID = _irmcat.GetNewRMCategoryId().Result;
                        string result = _irmcat.Post(rmc).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(rmc);
            }
            catch (Exception ex)
            {
                return Json("CreateRMCategory " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditRMCategory")]
        public IActionResult EditRMCategory(RMCategory rmc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string result = _irmcat.Put(rmc).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(rmc);
            }
            catch (Exception ex)
            {
                return Json("EditRMCategory " + ex.Message);
            }
        }

        public ActionResult EditRMCategory(int id)
        {
            try
            {
                _rmcat = new RMCategory();
                _rmcat = _irmcat.Get(id).Result;
                return View(_rmcat);
            }
            catch (Exception ex)
            {
                return Json("EditRMCategory " + ex.Message);
            }
        }

        public ActionResult DeleteRMCategory(int id)
        {
            try
            {
                _irmcat.Delete(id);
                return RedirectToAction("RMCategoryList");
            }
            catch (Exception ex)
            {
                return Json("DeleteRMCategory " + ex.Message);
            }
        }
    }
}
