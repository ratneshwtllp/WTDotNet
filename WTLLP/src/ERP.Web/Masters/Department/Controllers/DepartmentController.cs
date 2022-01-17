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

namespace ERP.Web.Areas.Department.Controllers
{
    [AuthLogin]
    [Area("Department")]
    public class DepartmentController : Controller
    {
        DepartmentDetails _Department = new DepartmentDetails();
        IHostingEnvironment _env;
        IDepartmentContract _iDepartment;

        public DepartmentController(IHostingEnvironment env, IDepartmentContract un)
        {
            _env = env;
            _iDepartment = un;
        }

        public ActionResult DepartmentList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("DepartmentList " + ex.Message);
            }
        }

        public ActionResult DepartmentList_Partial(String search)
        {
            try
            {
                return PartialView("_DepartmentList", _iDepartment.GetDepartmentList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("DepartmentList_Partial " + ex.Message);
            }
        }

        public ActionResult CreateDepartment()
        {
            try
            {
                _Department = new DepartmentDetails();
                return View(_Department);
            }
            catch (Exception ex)
            {
                return Json("CreateDepartment " + ex.Message);
            }
        }

        public ActionResult DeleteDepartment(int id)
        {
            try
            {
                _iDepartment.Delete(id);
                return RedirectToAction("DepartmentList");
            }
            catch (Exception ex)
            {
                return Json("DeleteDepartment " + ex.Message);
            }
        }

        public ActionResult EditDepartment(int id)
        {
            try
            {
                _Department = new DepartmentDetails();
                _Department = _iDepartment.Get(id).Result;
                return View(_Department);
            }
            catch (Exception ex)
            {
                return Json("EditDepartment " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateDepartment(DepartmentDetails DD)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _iDepartment.CheckDuplicate(DD.DepartmentName ).Result;
                    if (duplicate == 0)
                    {
                        DD.EntryDate = DateTime.Now.Date;
                        DD.SessionYear = Helper.Create_Session();
                        DD.UserId = HttpContext.Session.GetInt32("userid").Value;
                        DD.DepartmentID   = _iDepartment.GetNewDepartmentId().Result;
                        string result = _iDepartment.Post(DD).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(DD);
            }
            catch (Exception ex)
            {
                return Json("CreateDepartment " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditDepartment")]
        public IActionResult EditDepartment(DepartmentDetails DD)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string result = _iDepartment.Put(DD).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(DD);
            }
            catch (Exception ex)
            {
                return Json("EditDepartment " + ex.Message);
            }
        }
    }
}
