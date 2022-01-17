using ERP.Web.Views.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Hosting; 
using System;

namespace ERP.Web.Controllers
{
    public class LoginController : Controller
    {
        UserDetails _user = new UserDetails();
        IHostingEnvironment _env;
        IUserContract _iuser;
        IDBBackup _ibackup;
        public LoginController(IHostingEnvironment env, IUserContract usr, IDBBackup ibackup)
        {
            _env = env;
            _iuser = usr;
            _ibackup = ibackup;
        }

        public ActionResult Login()
        {
            try
            {
                ViewData["error"] = TempData["info"];
                return View("Login");   
            }
            catch (Exception ex)
            {
                return Json("Login " + ex.Message);
            }
        }


        [HttpPost]
        public ActionResult Login(UserModel USR)
        {
            try
            {
                int userid = _iuser.GetId(USR.UName, USR.Password).Result;
                if (userid > 0)
                {
                    HttpContext.Session.SetInt32("userid", userid);
                    HttpContext.Session.SetString("username", USR.UName);

                    ViewUserDetails _viewusr = new ViewUserDetails();
                    _viewusr = _iuser.GetUserDetails(userid).Result;
                    if (_viewusr.IsActive == 0)
                    {
                        TempData["info"] = "User id is disabled , please contcat your admin.";
                        return RedirectToAction("Login", "Login");
                    }
                    //HttpContext.Session.SetString("usernamewithbranchcode", _viewusr.UserName + "[" + _viewusr.BranchCode + "]");
                    HttpContext.Session.SetInt32("usertypeid", _viewusr.UserTypeId);
                    return RedirectToAction("Index", "Home", new { area = "Home" });
                }
                else
                {
                    TempData["info"] = "Wrong username or password";
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                return Json("Login " + ex.Message);
            }
        }

        public ActionResult LogOut()
        {
            try
            {
                HttpContext.Session.SetInt32("userid", 0);
                HttpContext.Session.SetString("username", "-");
                HttpContext.Session.SetInt32("usertypeid", 0);

                string result = _ibackup.Create().Result;
                return RedirectToAction("Login", "Login");
            }
            catch (Exception ex)
            {
                return Json("LogOut " + ex.Message);
            }
        }
    }
}
