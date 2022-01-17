using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ERP.Web.Areas.Structure.Controllers
{
    [AuthLogin]
    [Area("Structure")]
    public class StructureController : Controller
    {
        StructureDetails _structure = new StructureDetails();
        IHostingEnvironment _env;
        IStrucureContract _istructure;

        public StructureController(IHostingEnvironment env, IStrucureContract struc)
        {
            _env = env;
            _istructure = struc;
        }

        public ActionResult CreateStructure()
        {
            try
            {
                _structure = new StructureDetails();
                return View(_structure);
            }
            catch (Exception ex)
            {
                return Json("CreateStructure " + ex.Message);
            }
        }

        public ActionResult StructureList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("StructureList " + ex.Message);
            }
        }

        public ActionResult StructureList_Partial(String search)
        {
            try
            {
                return PartialView("_StructureList", _istructure.GetStructureList(search).Result);

            }
            catch(Exception ex)
            {
                return Json("StructureList_Partial " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateStructure(StructureDetails ST)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _istructure.CheckDuplicate(ST.Tsname).Result;
                    if (duplicate == 0)
                    {
                        ST.EntryDate = DateTime.Now.Date;
                        ST.SessionYear = Helper.Create_Session();
                        ST.UserId = HttpContext.Session.GetInt32("userid").Value;
                        ST.Tsid = _istructure.GetNewStructureId().Result;
                        string result = _istructure.Post(ST).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(ST);
            }
            catch (Exception ex)
            {
                return Json("CreateStructure " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditStructure")]
        public IActionResult EditStructure(StructureDetails ST)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string result = _istructure.Put(ST).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(ST);
            }
            catch (Exception ex)
            {
                return Json("EditStructure " + ex.Message);
            }
        }

        public ActionResult EditStructure(int id)
        {
            try
            {
                _structure = new StructureDetails();
                _structure = _istructure.Get(id).Result;
                return View(_structure);
            }
            catch (Exception ex)
            {
                return Json("EditStructure " + ex.Message);
            }
        }

        public ActionResult DeleteStructure(int id)
        {
            try
            {
                _istructure.Delete(id);
                return RedirectToAction("StructureList");
            }
            catch (Exception ex)
            {
                return Json("DeleteStructure " + ex.Message);
            }
        }
    }
}
