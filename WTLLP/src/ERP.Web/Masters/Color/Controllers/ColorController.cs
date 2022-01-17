using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ERP.Web.Areas.Color.Controllers
{
    [AuthLogin]
    [Area("Color")]
    public class ColorController : Controller
    {
        ColorDetails _color = new ColorDetails();
        IHostingEnvironment _env;
        IColorContract _icolor;

        public ColorController(IHostingEnvironment env, IColorContract cl)
        {
            _env = env;
            _icolor = cl;
        }

        public ActionResult CreateColor()
        {
            try
            {
                _color = new ColorDetails();
                return View(_color);
            }
            catch (Exception ex)
            {
                return Json("CreateColor " + ex.Message);
            }
        }

        public ActionResult ColorList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("ColorList " + ex.Message);
            }
        }

        public ActionResult ColorList_Partial(String search)
        {
            try
            {
                return PartialView("_ColorList", _icolor.GetColorList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("ColorList_Partial " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateColor(ColorDetails CL)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _icolor.CheckDuplicate(CL.Clname).Result;
                    if (duplicate == 0)
                    {
                        CL.Online_Transfer = 0;
                        CL.EntryDate = DateTime.Now.Date;
                        CL.SessionYear = Helper.Create_Session();
                        CL.UserId = HttpContext.Session.GetInt32("userid").Value;
                        CL.Clid = _icolor.GetNewColorId().Result;
                        string result = _icolor.Post(CL).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(CL);
            }
            catch (Exception ex)
            {
                return Json("CreateColor " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditColor")]
        public IActionResult EditColor(ColorDetails CL)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CL.Online_Transfer = 0;
                    string result = _icolor.Put(CL).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(CL);
            }
            catch (Exception ex)
            {
                return Json("EditColor " + ex.Message);
            }
        }

        public ActionResult EditColor(int id)
        {
            try
            {
                _color = new ColorDetails();
                _color = _icolor.Get(id).Result;
                return View(_color);
            }
            catch (Exception ex)
            {
                return Json("EditColor " + ex.Message);
            }
        }

        public ActionResult DeleteColor(int id)
        {
            try
            {
                _icolor.Delete(id);
                return RedirectToAction("ColorList");
            }
            catch (Exception ex)
            {
                return Json("DeleteColor " + ex.Message);
            }
        }
    }
}
