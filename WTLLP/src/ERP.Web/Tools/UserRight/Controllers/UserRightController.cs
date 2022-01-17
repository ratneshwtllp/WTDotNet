using ERP.Business.Contracts;
using ERP.Domain.Entities;
using ERP.Web.Areas.UserRight.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ERP.Web.Areas.UserRight.Controllers
{
    [AuthLogin]
    [Area("UserRight")]
    public class UserRightController : Controller
    {
        UserRights _user = new UserRights();
        IHostingEnvironment _env;
        IUserRightContract _iuser;

        public UserRightController(IHostingEnvironment env, IUserRightContract user)
        {
            _env = env;
            _iuser = user;
        }

        public ActionResult Create()
        {
            try
            {
                UserModel _userm = new UserModel();
                _userm.UserList = Helper.ConvertObjectToSelectList(_iuser.GetUserList().Result);
                _userm.MenuCategoryList = Helper.ConvertObjectToSelectList(_iuser.GetMenuCatList().Result);
                return View(_userm);
            }
            catch (Exception ex)
            {
                return Json("Create " + ex.Message);
            }
        }
        
        public ActionResult SearchMenuChekedList(int menucatid)
        {
            try
            {
                UserModel _user = new UserModel();
                List<MenuDetails> r = _iuser.SearchMenuChekedList(menucatid).Result;
                List<UserRightModel> _UserrightModel = new List<UserRightModel>();
                UserRightModel _onemenu;
                for (var i = 0; i < r.Count; i++)
                {
                    _onemenu = new UserRightModel();
                    _onemenu.MenuId = r[i].MenuID;
                    _onemenu.MenuName = r[i].MenuName;
                    _onemenu.Selected = false;
                    lock (_UserrightModel)
                    {
                        _UserrightModel.Add(_onemenu);
                    }
                }
                _user.UserRightModel = _UserrightModel;
                return View("_SearchMenuCheckedList", _user);
            }
            catch (Exception ex)
            {
                return Json("SearchMenuChekedList " + ex.Message);
            }
        }

        public ActionResult ShowAssignedMenu(int userid)
        {
            try
            {
                UserModel _user = new UserModel();

                List<ViewUserRights> _listviewuserright = new List<ViewUserRights>();
                _listviewuserright = _iuser.GetViewMenuList(userid).Result;

                List<UserRightModel> _UserRightModel = new List<UserRightModel>();
                UserRightModel _oneMenu;
                for (var i = 0; i < _listviewuserright.Count; i++)
                {
                    _oneMenu = new UserRightModel();
                    _oneMenu.MenuId = _listviewuserright[i].MenuID;
                    _oneMenu.MenuName = _listviewuserright[i].MenuName;
                    _oneMenu.Selected = false;
                    lock (_UserRightModel)
                    {
                        _UserRightModel.Add(_oneMenu);
                    }
                }
                _user.UserRightModel = _UserRightModel;
                return View("_SearchMenuCheckedList", _user);
            }
            catch (Exception ex)
            {
                return Json("ShowAssignedMenu " + ex.Message);
            }
        }

        [HttpPost]
        public JsonResult AssignMenutoUser([FromBody]UserModel user)
        {
            try
            {
                List<UserRights> _listuserright = new List<UserRights>();
                UserRights _userright;
                string result = "";
                long userrightid = _iuser.GetNewId().Result;
                for (int i = 0; i < user.UserRightModel.Count; i++)
                {
                    _userright = new UserRights();
                    _userright.UserRightsId = userrightid;
                    _userright.UserId = user.UserRightModel[i].UserId;
                    _userright.MenuId = user.UserRightModel[i].MenuId;
                    lock (_listuserright)
                    {
                        _listuserright.Add(_userright);
                    }
                    userrightid++;
                    result = _iuser.PostUserMenu(_userright).Result;
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json("AssignMenutoUser " + ex.Message);
            }
        }

        string result = "";
        [HttpPost]
        public JsonResult RemoveAssignedMenuFromMenu([FromBody]UserModel user)
        {
            try
            {
                List<UserRights> _listuserright = new List<UserRights>();
                UserRights _userright;
                for (int i = 0; i < user.UserRightModel.Count; i++)
                {
                    _userright = new UserRights();
                    _userright.UserId = user.UserRightModel[i].UserId;
                    _userright.MenuId = user.UserRightModel[i].MenuId;
                    lock (_listuserright)
                    {
                        _listuserright.Add(_userright);
                    }
                    result = _iuser.RemoveMenu(_userright).Result;
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json("RemoveAssignedMenuFromMenu " + ex.Message);
            }
        }

    }
}
