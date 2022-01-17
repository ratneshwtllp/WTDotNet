using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using ERP.Web.Areas.RMSubCat.Models;

namespace ERP.Web.Areas.RMSubCat.Controllers
{
    [AuthLogin]
    [Area("RMSubCat")]
    public class RMSubCatController : Controller
    {
        RMSubCategoryModel _subcatmodel = new RMSubCategoryModel();
        IHostingEnvironment _env;
        IRMSubCategoryContract _iRMSubCat;

        public RMSubCatController(IHostingEnvironment env, IRMSubCategoryContract rmc)
        {
            _env = env;
            _iRMSubCat = rmc;
        }

        public ActionResult CreateRMSubCategory()
        {
            try
            {
                _subcatmodel = new RMSubCategoryModel();
                _subcatmodel.RMCategoryList = Helper.ConvertObjectToSelectList(_iRMSubCat.GetCategoryList().Result);
                _subcatmodel.HSNList = Helper.ConvertObjectToSelectList(_iRMSubCat.GetHSNList().Result);
                return View(_subcatmodel);
            }
            catch (Exception ex)
            {
                return Json("CreateRMSubCategory " + ex.Message);
            }
        }

        public ActionResult RMSubCategoryList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("RMSubCategoryList " + ex.Message);
            }
        }

        public ActionResult RMSubCategoryList_Partial(String search)
        {
            try
            {
                return PartialView("_RMSubCategoryList", _iRMSubCat.GetRMSubCategoryList(search).Result);

            }
            catch (Exception ex)
            {
                return Json("RMSubCategoryList_Partial " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateRMSubCategory(RMSubCategoryModel rmc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _iRMSubCat.CheckDuplicate(rmc.SubCategoryName).Result;
                    if (duplicate == 0)
                    {
                        RMSubCategory _subcate = new RMSubCategory();
                        _subcate.SubCategoryID = _iRMSubCat.GetNewRMSubCategoryId().Result;
                        _subcate.CategoryID = rmc.CategoryID;
                        _subcate.Description = rmc.Description;
                        _subcate.SubCategoryName = rmc.SubCategoryName;
                        _subcate.HSNId = rmc.HSNId;

                        string result = _iRMSubCat.Post(_subcate).Result;
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
                return Json("CreateRMSubCategory " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditRMSubCategory")]
        public IActionResult EditRMSubCategory(RMSubCategoryModel rmc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RMSubCategory _subcate = new RMSubCategory();
                    _subcate.SubCategoryID = rmc.SubCategoryID;
                    _subcate.CategoryID = rmc.CategoryID;
                    _subcate.Description = rmc.Description;
                    _subcate.SubCategoryName = rmc.SubCategoryName;
                    _subcate.HSNId = rmc.HSNId;
                    string result = _iRMSubCat.Put(_subcate).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(rmc);
            }
            catch (Exception ex)
            {
                return Json("EditRMSubCategory " + ex.Message);
            }
        }

        public ActionResult EditRMSubCategory(int id)
        {
            try
            {
                RMSubCategory _subcate = new RMSubCategory();
                _subcate = _iRMSubCat.Get(id).Result;

                _subcatmodel = new RMSubCategoryModel();
                _subcatmodel.RMCategoryList = Helper.ConvertObjectToSelectList(_iRMSubCat.GetCategoryList().Result);
                _subcatmodel.HSNList = Helper.ConvertObjectToSelectList(_iRMSubCat.GetHSNList().Result);

                _subcatmodel.SubCategoryID = _subcate.SubCategoryID;
                _subcatmodel.CategoryID = _subcate.CategoryID;
                _subcatmodel.SubCategoryName = _subcate.SubCategoryName;
                _subcatmodel.Description = _subcate.Description;
                _subcatmodel.HSNId = _subcate.HSNId;

                return View(_subcatmodel);
            }
            catch (Exception ex)
            {
                return Json("EditRMSubCategory " + ex.Message);
            }
        }

        public ActionResult DeleteRMSubCategory(int id)
        {
            try
            {
                _iRMSubCat.Delete(id);
                return RedirectToAction("RMSubCategoryList");
            }
            catch (Exception ex)
            {
                return Json("DeleteRMSubCategory " + ex.Message);
            }
        }

        public JsonResult RefreshCategory()
        {
            try
            {
                return Json(_iRMSubCat.GetCategoryList().Result);
            }
            catch (Exception ex)
            {
                return Json("RefreshCategory " + ex.Message);
            }
        }
        public JsonResult RefreshHSN() 
        {
            try
            {
                return Json(_iRMSubCat.GetHSNList().Result);
            }
            catch (Exception ex)
            {
                return Json("RefreshHSN " + ex.Message);
            }
        }

    }
}
