using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ERP.Web.Areas.Selection.Controllers
{
    [AuthLogin]
    [Area("Selection")]
    public class SelectionController : Controller
    {
        SelectionDetails _selection = new SelectionDetails();
        IHostingEnvironment _env;
        ISelectionContract _iselection;

        public SelectionController(IHostingEnvironment env, ISelectionContract selec)
        {
            _env = env;
            _iselection = selec;
        }

        public ActionResult CreateSelection()
        {
            try
            {
                _selection = new SelectionDetails();
                return View(_selection);
            }
            catch (Exception ex)
            {
                return Json("CreateSelection " + ex.Message);
            }
        }

        public ActionResult SelectionList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("SelectionList " + ex.Message);
            }
        }

        public ActionResult SelectionList_Partial(String search)
        {
            try
            {
                return PartialView("_SelectionList", _iselection.GetSelectionList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("SelectionList_Partial " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateSelection(SelectionDetails sl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _iselection.CheckDuplicate(sl.Selection).Result;
                    if (duplicate == 0)
                    {
                        sl.EntryDate = DateTime.Now.Date;
                        sl.SessionYear = Helper.Create_Session();
                        sl.UserId = HttpContext.Session.GetInt32("userid").Value;
                        sl.EntryDate = DateTime.Now.Date;
                        sl.SelectionID = _iselection.GetNewSelectionId().Result;
                        string result = _iselection.Post(sl).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(sl);
            }
            catch (Exception ex)
            {
                return Json("CreateSelection " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditSelection")]
        public IActionResult EditSelection(SelectionDetails sl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string result = _iselection.Put(sl).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(sl);
            }
            catch (Exception ex)
            {
                return Json("EditSelection " + ex.Message);
            }
        }

        public ActionResult EditSelection(int id)
        {
            try
            {
                _selection = new SelectionDetails();
                _selection = _iselection.Get(id).Result;
                return View(_selection);
            }
            catch (Exception ex)
            {
                return Json("EditSelection " + ex.Message);
            }
        }

        public ActionResult DeleteSelection(int id)
        {
            try
            {
                _iselection.Delete(id);
                return RedirectToAction("SelectionList");
            }
            catch (Exception ex)
            {
                return Json("DeleteSelection " + ex.Message);
            }
        }
    }
}
