using ERP.Business.Contracts;
using ERP.Domain.Entities;
using ERP.Web.Areas.User.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ERP.Web.Areas.User.Controllers
{
    [AuthLogin]
    [Area("User")]
    public class UserController : Controller
    {
        UserDetails _user = new UserDetails();
        IHostingEnvironment _env;
        IUserContract _iuser;

        public UserController(IHostingEnvironment env, IUserContract usr)
        {
            _env = env;
            _iuser = usr;
        }

        public ActionResult UserList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("UserList " + ex.Message);
            }
        }

        public ActionResult UserList_Partial(String search)
        {
            try
            {
                return PartialView("_UserList", _iuser.GetUserList(search).Result);

            }
            catch (Exception ex)
            {
                return Json("UserList_Partial " + ex.Message);
            }
        }

        public ActionResult CreateUser()
        {
            try
            {
                //_user = new UserDetails();
                //return View(_user);

                UserModel _usermodel = new UserModel();
                _usermodel.DepartmentList = Helper.ConvertObjectToSelectList(_iuser.GetDepartmentList().Result);
                _usermodel.UserTypeList = Helper.ConvertObjectToSelectList(_iuser.GetUserTypeList().Result);

                return View(_usermodel);
            }
            catch (Exception ex)
            {
                return Json("CreateUser " + ex.Message);
            }
        }

        public ActionResult EditUser(int id)
        {
            try
            {
                _user = new UserDetails();
                _user = _iuser.Get(id).Result;
                UserModel _usermodel = new UserModel();
                _usermodel.UserId = _user.UserId;
                _usermodel.UserName = _user.UserName;
                _usermodel.LoginName = _user.LoginName;
                _usermodel.Password = _user.Password;
                _usermodel.ConfirmPassword = _user.ConfirmPassword;
                _usermodel.Address = _user.Address;
                _usermodel.Email = _user.Email;
                _usermodel.PhoneNo = _user.PhoneNo;
                _usermodel.MobileNo = _user.MobileNo;
                _usermodel.DepartmentList = Helper.ConvertObjectToSelectList(_iuser.GetDepartmentList().Result);
                _usermodel.DepartmentId = _user.DepartmentId;
                _usermodel.UserTypeList = Helper.ConvertObjectToSelectList(_iuser.GetUserTypeList().Result);

                _usermodel.UserTypeId = _user.UserTypeId;
                if (_user.IsActive == 1)
                {
                    _usermodel.ChkIsActive = true;
                }
                _usermodel.IsActive = _user.IsActive;
                //_usermodel.DepartmentHead = _user.DepartmentHead;
                return View(_usermodel);
            }
            catch (Exception ex)
            {
                return Json("EditUser " + ex.Message);
            }
        }

        public ActionResult DeleteUser(int id)
        {
            try
            {
                _iuser.Delete(id);
                return RedirectToAction("UserList");
            }
            catch (Exception ex)
            {
                return Json("DeleteUser " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateUser(UserModel userm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _user = new UserDetails();
                    int duplicate = 0;
                    duplicate = _iuser.CheckDuplicate(userm.UserName).Result;
                    if (duplicate == 0)
                    {
                        if (userm.Password != userm.ConfirmPassword)
                        {
                            //RedirectToAction("CreateUser");
                            //return BadRequest("2");
                            return Ok("1");
                        }

                        _user.EntryDate = DateTime.Now.Date;
                        _user.SessionYear = Helper.Create_Session();
                        _user.UserId = _iuser.GetNewUserId().Result;
                        _user.UserName = userm.UserName;
                        _user.LoginName = userm.LoginName;
                        _user.Password = userm.Password;
                        _user.ConfirmPassword = userm.ConfirmPassword;
                        _user.Address = userm.Address;
                        _user.Email = userm.Email;
                        _user.PhoneNo = userm.PhoneNo;
                        _user.MobileNo = userm.MobileNo;
                        _user.DepartmentId = userm.DepartmentId;
                        _user.UserTypeId = userm.UserTypeId;
                        _user.DepartmentHead = 0;
                        if (userm.ChkIsActive == true)
                        {
                            _user.IsActive = 1;
                        }
                        else
                        {
                            _user.IsActive = 0;
                        }
                        //_user.DepartmentHead = userm.DepartmentHead;
                        string result = _iuser.Post(_user).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(userm);
            }
            catch (Exception ex)
            {
                return Json("CreateUser " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditUser")]
        public IActionResult EditUser(UserModel userm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _user = new UserDetails();
                    if (userm.Password != userm.ConfirmPassword)
                    {
                        //RedirectToAction("CreateUser");
                        //return BadRequest("2");
                        return Ok("1");
                    }
                    _user.UserId = userm.UserId;
                    _user.UserName = userm.UserName;
                    _user.LoginName = userm.LoginName;
                    _user.Password = userm.Password;
                    _user.ConfirmPassword = userm.ConfirmPassword;
                    _user.Address = userm.Address;
                    _user.Email = userm.Email;
                    _user.PhoneNo = userm.PhoneNo;
                    _user.MobileNo = userm.MobileNo;
                    _user.DepartmentId = userm.DepartmentId;
                    _user.UserTypeId = userm.UserTypeId;
                    _user.DepartmentHead = 0;
                    if (userm.ChkIsActive == true)
                    {
                        _user.IsActive = 1;
                    }
                    else
                    {
                        _user.IsActive = 0;
                    }
                    //_user.DepartmentHead = userm.DepartmentHead;
                    string result = _iuser.Put(_user).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(userm);
            }
            catch (Exception ex)
            {
                return Json("EditUser " + ex.Message);
            }
        }
    }
}
