using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ERP.Web.Areas.Carton.Controllers
{
    [AuthLogin]
    [Area("Carton")]
    public class CartonController : Controller
    {
        CartonDetails _carton = new CartonDetails();
        IHostingEnvironment _env;
        ICartonContract _icarton;

        public CartonController(IHostingEnvironment env, ICartonContract icarton)
        {
            _env = env;
            _icarton = icarton;
        }

        public ActionResult CreateCarton()
        {
            try
            {
                _carton = new CartonDetails();
                return View(_carton);
            }
            catch (Exception ex)
            {
                return Json("CreateCarton " + ex.Message);
            }
        }

        public ActionResult CartonList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("CartonList " + ex.Message);
            }
        }

        public ActionResult CartonList_Partial(String search)
        {
            try
            {
                return PartialView("_CartonList", _icarton.GetCartonList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("CartonList_Partial " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateCarton(CartonDetails carton)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    carton.SessionYear = Helper.Create_Session();
                    carton.UserId = HttpContext.Session.GetInt32("userid").Value;
                    carton.CartonId = _icarton.GetNewCartonId().Result;
                    carton.BrandId = 0;
                    carton.OuterInner = 0;
                    string result = _icarton.Post(carton).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(carton);
            }
            catch (Exception ex)
            {
                return Json("CreateCarton " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditCarton")]
        public IActionResult EditCarton(CartonDetails carton)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string result = _icarton.Put(carton).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(carton);
            }
            catch (Exception ex)
            {
                return Json("EditCarton " + ex.Message);
            }
        }

        public ActionResult EditCarton(int id)
        {
            try
            {
                _carton = new CartonDetails();
                _carton = _icarton.Get(id).Result;
                return View(_carton);
            }
            catch (Exception ex)
            {
                return Json("EditCarton " + ex.Message);
            }
        }

        public ActionResult DeleteCarton(int id)
        {
            try
            {
                _icarton.Delete(id);
                return RedirectToAction("CartonList");
            }
            catch (Exception ex)
            {
                return Json("DeleteCarton " + ex.Message);
            }
        }
    }
}
