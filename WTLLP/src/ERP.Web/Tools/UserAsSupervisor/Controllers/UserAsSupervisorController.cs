using ERP.Business.Contracts;
using ERP.Domain.Entities;
using ERP.Web.Areas.UserAsSupervisor.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ERP.Web.Areas.Contractor.Models;
using Microsoft.AspNetCore.Http;

namespace ERP.Web.Areas.UserAsSupervisor.Controllers
{
    [AuthLogin]
    [Area("UserAsSupervisor")]
    public class UserAsSupervisorController : Controller
    {
        UserRights _user = new UserRights();
        IHostingEnvironment _env;
        IUserAsSupervisorContract _iuser;

        public UserAsSupervisorController(IHostingEnvironment env, IUserAsSupervisorContract user)
        {
            _env = env;
            _iuser = user;
        }

        public ActionResult CreateUserAsSupervisor()
        {
            try
            {
                UserAsSupervisorModel _userm = new UserAsSupervisorModel();
                _userm.UserList = Helper.ConvertObjectToSelectList(_iuser.GetUserList().Result);
                return View(_userm);
            }
            catch (Exception ex)
            {
                return Json("CreateUserAsSupervisor " + ex.Message);
            }
        }

        public ActionResult AssigneContractorList(int userid)
        {
            try
            {
                UserAsSupervisorModel _user = new UserAsSupervisorModel();
                List<ContractorDetails> r = _iuser.GetContractorList().Result;
                List<UserAsSupervisorDetails> existing = _iuser.GetUserSupervisor(userid).Result;
                List<ContractorModel> _contactorModellist = new List<ContractorModel>();
                ContractorModel _onecontractor;
                bool contactor_found = false;
                for (var i = 0; i < r.Count; i++)
                {
                    contactor_found = false;
                    _onecontractor = new ContractorModel();
                    _onecontractor.ContractorID = r[i].ContractorID;
                    _onecontractor.ContractorName = r[i].ContractorName;
                    _onecontractor.Selected = false;
                    for (var j = 0; j < existing.Count; j++)
                    {
                        if (existing[j].ContractorID == r[i].ContractorID)
                        {
                            contactor_found = true;
                            break;
                        }
                    }

                    if (contactor_found)
                        _onecontractor.Selected = true;
                    lock (_contactorModellist)
                    {
                        _contactorModellist.Add(_onecontractor);
                    }
                }
                _user.ContractorModel = _contactorModellist;
                return View("_AssigneContactorList", _user);
            }
            catch (Exception ex)
            {
                return Json("AssigneContractorList " + ex.Message);
            }
        }

      

        [HttpPost]
        public JsonResult AssigneUserContractor([FromBody]UserAsSupervisorModel user)
        {
            try
            {
                List<UserAsSupervisorDetails> _listuserassupervisor= new List<UserAsSupervisorDetails>();
                UserAsSupervisorDetails _userassupervisor;
                string result = "";
                long userrightid = _iuser.GetNewId().Result;
                for (int i = 0; i < user.ContractorModel.Count; i++)
                {
                    _userassupervisor = new UserAsSupervisorDetails();
                    _userassupervisor.UserAsSupervisorId = userrightid;
                    _userassupervisor.UserId = user.ContractorModel[i].UserId;
                    _userassupervisor.ContractorID = user.ContractorModel[i].ContractorID;
                    _userassupervisor.EntryDate = DateTime.Now.Date;
                    _userassupervisor.SessionYear = Helper.Create_Session();
                    _userassupervisor.LoginUserId= HttpContext.Session.GetInt32("userid").Value;
                    lock (_listuserassupervisor)
                    {
                        _listuserassupervisor.Add(_userassupervisor);
                    }
                    userrightid++;
                   
                }
                result = _iuser.Post(_listuserassupervisor).Result;
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json("AssigneUserContractor " + ex.Message);
            }
        }

       
    }
}
