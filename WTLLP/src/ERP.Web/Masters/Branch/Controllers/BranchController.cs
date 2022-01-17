using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using ERP.Web.Areas.Branch.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;

namespace ERP.Web.Areas.Branch.Controllers
{
    [AuthLogin]
    [Area("Branch")]
    public class BranchController : Controller
    {
        BranchDetails _branch = new BranchDetails();
        BranchModel _branchmodel = new BranchModel();
        IHostingEnvironment _env;
        IBranchContract _iBranch;

        public BranchController(IHostingEnvironment env, IBranchContract IBC)
        {
            _env = env;
            _iBranch = IBC;
        }

        public ActionResult BranchList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("BranchList " + ex.Message);
            }
        }

        public ActionResult BranchList_Partial(String search)
        {
            try
            {
                return PartialView("_BranchList", _iBranch.GetBranchList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("BranchList_Partial " + ex.Message);
            }
        }

        public ActionResult CreateBranch()
        {
            try
            {
                _branchmodel = new BranchModel();

                _branchmodel.StateList = Helper.ConvertObjectToSelectList(_iBranch.GetListState().Result);
                _branchmodel.CityList = Helper.FillDropDownList_Empty();
                return View(_branchmodel);
            }
            catch (Exception ex)
            {
                return Json("CreateBranch " + ex.Message);
            }
        }

        [HttpGet]
        public JsonResult GetCityList(int id)
        {
            try
            {
                return Json(_iBranch.GetListCity(id).Result);
            }
            catch (Exception ex)
            {
                return Json("GetCityList " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateBranch(BranchModel BM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _iBranch.CheckDuplicate(BM.BranchName).Result;
                    if (duplicate == 0)
                    {
                        BranchDetails _branch = new BranchDetails();
                        _branch.BranchID = _iBranch.GetNewBranchId().Result;
                        _branch.BranchName = BM.BranchName;
                        _branch.Address = BM.Address;
                        _branch.PhoneNo = BM.PhoneNo;
                        _branch.StateId = BM.StateId;
                        _branch.CityId = BM.CityId;

                        _branch.EntryDate = DateTime.Now.Date;
                        _branch.SessionYear = Helper.Create_Session();
                        _branch.UserId = HttpContext.Session.GetInt32("userid").Value;

                        string result = _iBranch.Post(_branch).Result;
                        ModelState.Clear();
                        return RedirectToAction("CreateBranch");
                    } 
                }
                return View(BM);
            }
            catch (Exception ex)
            {
                return Json("CreateBranch " + ex.Message);
            }
        }

        public ActionResult EditBranch(int id)
        {
            try
            {
                _branch = new BranchDetails();
                _branch = _iBranch.Get(id).Result;

                _branchmodel = new BranchModel();
                _branchmodel.BranchID = _branch.BranchID;
                _branchmodel.BranchName = _branch.BranchName;
                _branchmodel.Address = _branch.Address;
                _branchmodel.PhoneNo = _branch.PhoneNo;

                _branchmodel.StateId = _branch.StateId;
                _branchmodel.CityId = _branch.CityId;
                _branchmodel.StateList = Helper.ConvertObjectToSelectList(_iBranch.GetListState().Result);
                _branchmodel.CityList = Helper.ConvertObjectToSelectList(_iBranch.GetListCity(_branch.StateId).Result);

                return View(_branchmodel);
            }
            catch (Exception ex)
            {
                return Json("EditBranch " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditBranch")]
        public IActionResult EditBranch(BranchModel BM)
        {
            try
            {
                if (ModelState.IsValid)
                { 
                    _branch = new BranchDetails();
                    _branch.BranchID = BM.BranchID;
                    _branch.BranchName = BM.BranchName;
                    _branch.Address = BM.Address;
                    _branch.PhoneNo = BM.PhoneNo;
                    _branch.StateId = BM.StateId;
                    _branch.CityId = BM.CityId;

                    string result = _iBranch.Put(_branch).Result;
                    ModelState.Clear();
                    return RedirectToAction("BranchList", "Branch");
                }
                return View(BM);
            }
            catch (Exception ex)
            {
                return Json("EditBranch " + ex.Message);
            }
        }
         
        public ActionResult DeleteBranch(int id)
        {
            try
            {
                _iBranch.Delete(id);
                return RedirectToAction("BranchList");
            }
            catch (Exception ex)
            {
                return Json("DeleteBranch " + ex.Message);
            }
        }

        public JsonResult RefreshState() 
        {
            try
            {
                return Json(_iBranch.GetListState().Result); 
            }
            catch (Exception ex)
            {
                return Json("RefreshState " + ex.Message);
            }
        }
    }
}
