using ERP.Business.Contracts;
using ERP.Domain.Entities;
using ERP.Web.Areas.Menu.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ERP.Web.ViewComponents
{
    public class UserMenuListViewComponent : ViewComponent
    {
        MenuModel _Menumodel = new MenuModel();
        IHostingEnvironment _env;
        IUserRightContract _iMenucon;

        public UserMenuListViewComponent(IHostingEnvironment env, IUserRightContract IM)
        {
            _env = env;
            _iMenucon = IM;
        }

        public IViewComponentResult Invoke()
        {
            try
            {
                MenuModel _menumdel = new MenuModel();
                List<UserMenuCat> CatList = new List<UserMenuCat>();
                List<UserMenu> MenuList = new List<UserMenu>();

                //if (HttpContext.Session == null)
                //{
                //    return  View("Login", "Login");
                //}
                int userid = HttpContext.Session.GetInt32("userid").Value;
                if (userid != 1)
                {
                    List<ViewUserRights> UR = new List<ViewUserRights>();
                    UR = _iMenucon.GetViewMenuList(Convert.ToInt32(userid)).Result;

                    UserMenuCat Cat = new UserMenuCat();
                    UserMenu M;
                    for (int i = 0; i < UR.Count; i++)
                    {
                        if (i > 0)
                        {
                            if (UR[i].MenuCategoryID != UR[i - 1].MenuCategoryID)
                            {
                                Cat.UserMenu = MenuList;
                                lock (CatList)
                                {
                                    CatList.Add(Cat);
                                }

                                Cat = new UserMenuCat();
                                MenuList = new List<UserMenu>();

                                Cat.MenuCategoryID = UR[i].MenuCategoryID;
                                Cat.MenuCategoryName = UR[i].MenuCategoryName;

                                M = new UserMenu();
                                M.MenuCategoryID = 1;
                                M.MenuID = UR[i].MenuID;
                                M.MenuArea = UR[i].MenuArea;
                                M.MenuController = UR[i].MenuController;
                                M.MenuAction = UR[i].MenuAction;
                                M.MenuName = UR[i].MenuName;
                                lock (MenuList)
                                {
                                    MenuList.Add(M);
                                }
                            }
                            else
                            {
                                M = new UserMenu();
                                M.MenuCategoryID = 1;
                                M.MenuID = UR[i].MenuID;
                                M.MenuArea = UR[i].MenuArea;
                                M.MenuController = UR[i].MenuController;
                                M.MenuAction = UR[i].MenuAction;
                                M.MenuName = UR[i].MenuName;
                                lock (MenuList)
                                {
                                    MenuList.Add(M);
                                }
                            }
                        }
                        else
                        {
                            Cat.MenuCategoryID = UR[i].MenuCategoryID;
                            Cat.MenuCategoryName = UR[i].MenuCategoryName;

                            M = new UserMenu();
                            M.MenuCategoryID = 1;
                            M.MenuID = UR[i].MenuID;
                            M.MenuArea = UR[i].MenuArea;
                            M.MenuController = UR[i].MenuController;
                            M.MenuAction = UR[i].MenuAction;
                            M.MenuName = UR[i].MenuName;
                            lock (MenuList)
                            {
                                MenuList.Add(M);
                            }
                        }
                    }
                    //// for last record
                    Cat.UserMenu = MenuList;
                    lock (CatList)
                    {
                        CatList.Add(Cat);
                    }

                    _menumdel.UserMenuCat = CatList;
                    return View("UserMenu", _menumdel);
                }
                else
                {
                    return View("AdminMenu");
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
                //return View("Login", "Login");
            }
        }

    }
}
