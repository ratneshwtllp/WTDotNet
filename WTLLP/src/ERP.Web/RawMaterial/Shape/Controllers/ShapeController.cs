using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ERP.Web.Areas.Shape.Controllers
{
    [AuthLogin]
    [Area("Shape")]
    public class ShapeController : Controller
    {
        ShapeDetails _shape = new ShapeDetails();
        IHostingEnvironment _env;
        IShapeContract _ishape;

        public ShapeController(IHostingEnvironment env, IShapeContract sh)
        {
            _env = env;
            _ishape = sh;
        }

        public ActionResult CreateShape()
        {
            try
            {
                _shape = new ShapeDetails();
                return View(_shape);
            }
            catch (Exception ex)
            {
                return Json("CreateShape " + ex.Message);
            }
        }

        public ActionResult ShapeList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("ShapeList " + ex.Message);
            }
        }

        public ActionResult ShapeList_Partial(String search)
        {
            try
            {
                return PartialView("_ShapeList", _ishape.GetShapeList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("ShapeList_Partial " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateShape(ShapeDetails SH)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _ishape.CheckDuplicate(SH.ShapeName).Result;
                    if (duplicate == 0)
                    {
                        SH.EntryDate = DateTime.Now.Date;
                        SH.SessionYear = Helper.Create_Session();
                        SH.UserId = HttpContext.Session.GetInt32("userid").Value;
                        SH.ShapeId = _ishape.GetNewShapeId().Result;
                        string result = _ishape.Post(SH).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(SH);
            }
            catch (Exception ex)
            {
                return Json("CreateShape " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditShape")]
        public IActionResult EditShape(ShapeDetails SH)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string result = _ishape.Put(SH).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(SH);
            }
            catch (Exception ex)
            {
                return Json("EditShape " + ex.Message);
            }
        }

        public ActionResult EditShape(int id)
        {
            try
            {
                _shape = new ShapeDetails();
                _shape = _ishape.Get(id).Result;
                return View(_shape);
            }
            catch (Exception ex)
            {
                return Json("EditShape " + ex.Message);
            }
        }

        public ActionResult DeleteShape(int id)
        {
            try
            {
                _ishape.Delete(id);
                return RedirectToAction("ShapeList");
            }
            catch (Exception ex)
            {
                return Json("DeleteShape " + ex.Message);
            }
        }
    }
}
