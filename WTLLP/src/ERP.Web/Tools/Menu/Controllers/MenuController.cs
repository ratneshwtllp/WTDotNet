using ERP.Business.Contracts;
using ERP.Domain.Entities;
using ERP.Web.Areas.Menu.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ERP.Web.Areas.Menu.Controllers
{
    [AuthLogin]
    [Area("Menu")]
    public class MenuController : Controller
    {
        MenuModel _Menumodel = new MenuModel();
        IHostingEnvironment _env;
        IMenuContract _iMenucon;
        public MenuController(IHostingEnvironment env, IMenuContract IM)
        {
            _env = env;
            _iMenucon = IM;
        }

        public ActionResult Create()
        {
            try
            {
                _Menumodel = new MenuModel();
                _Menumodel.MenuCategoryNameList = Helper.ConvertObjectToSelectList(_iMenucon.MenuCategoryNameList().Result);
                return View(_Menumodel);
            }
            catch (Exception ex)
            {
                return Json("Create " + ex.Message);
            }
        }

        public ActionResult List()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("List " + ex.Message);
            }
        }

        public ActionResult List_Partial(string search)
        {
            try
            {
                return PartialView("_List", _iMenucon.GetList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("List_Partial " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Create(MenuModel MM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MenuDetails _Menu = new MenuDetails();
                    int duplicate = 0;
                    //duplicate = _iMenucon.CheckDuplicate(MM.MenuName).Result;
                    if (duplicate == 0)
                    {
                        _Menu.MenuID = _iMenucon.GetNewId().Result;
                        _Menu.MenuName = MM.MenuName;
                        _Menu.MenuCategoryID = MM.MenuCategoryID;
                        _Menu.MenuURL = "-";
                        _Menu.SessionYear = Helper.Create_Session();
                        _Menu.UserId = HttpContext.Session.GetInt32("userid").Value;
                        _Menu.EntryDate = DateTime.Now.Date;
                        _Menu.MenuArea = MM.MenuArea;
                        _Menu.MenuController = MM.MenuController;
                        _Menu.MenuAction = MM.MenuAction;
                        string result = _iMenucon.Post(_Menu).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(MM);
            }
            catch (Exception ex)
            {
                return Json("Create " + ex.Message);
            }
        }

        [HttpPost, ActionName("Edit")]
        public IActionResult Edit(MenuModel MM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MenuDetails _Menu = new MenuDetails();
                    _Menu.MenuID = MM.MenuID;
                    _Menu.MenuCategoryID = MM.MenuCategoryID;
                    _Menu.MenuURL = "-";
                    _Menu.MenuName = MM.MenuName;
                    _Menu.MenuArea = MM.MenuArea;
                    _Menu.MenuController = MM.MenuController;
                    _Menu.MenuAction = MM.MenuAction;
                    string result = _iMenucon.Put(_Menu).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(MM);
            }
            catch (Exception ex)
            {
                return Json("Edit " + ex.Message);
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                MenuDetails _Menu = new MenuDetails();
                _Menu = _iMenucon.Get(id).Result;
                _Menumodel = new MenuModel();
                _Menumodel.MenuCategoryNameList = Helper.ConvertObjectToSelectList(_iMenucon.MenuCategoryNameList().Result);
                _Menumodel.MenuID = _Menu.MenuID;
                _Menumodel.MenuCategoryID = _Menu.MenuCategoryID;
                _Menumodel.MenuName = _Menu.MenuName;
                _Menumodel.MenuURL = _Menu.MenuURL;
                _Menumodel.MenuArea = _Menu.MenuArea;
                _Menumodel.MenuController = _Menu.MenuController;
                _Menumodel.MenuAction = _Menu.MenuAction;
                return View(_Menumodel);
            }
            catch (Exception ex)
            {
                return Json("Edit " + ex.Message);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                _iMenucon.Delete(id);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                return Json("Delete " + ex.Message);
            }
        }
    }
}
