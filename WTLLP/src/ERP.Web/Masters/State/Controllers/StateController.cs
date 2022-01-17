using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using ERP.Web.Areas.State.Models;
using Microsoft.AspNetCore.Http;

namespace ERP.Web.Areas.State.Controllers
{
    [AuthLogin]
    [Area("State")]
    public class StateController : Controller
    {
        StateModel _statemodel = new StateModel();
        IHostingEnvironment _env;
        IStateContract _istate;

        public StateController(IHostingEnvironment env, IStateContract state)
        {
            _env = env;
            _istate = state;
        }
        
        public ActionResult StateList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("StateList " + ex.Message);
            }
        }

        public ActionResult StateList_Partial(String search)
        {
            try
            {
                return PartialView("_StateList", _istate.GetStateList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("StateList_Partial " + ex.Message);
            }
        }

        public ActionResult CreateState()
        {
            try
            {
                _statemodel = new StateModel();
                _statemodel.CountryList = Helper.ConvertObjectToSelectList(_istate.GetCountryList().Result);
                return View(_statemodel);
            }
            catch (Exception ex)
            {
                return Json("CreateState " + ex.Message);
            }
        }

        public ActionResult EditState(int id)
        {
            try
            {
                StateDetails _state = new StateDetails();
                _state = _istate.Get(id).Result;

                _statemodel = new StateModel();
                _statemodel.CountryList = Helper.ConvertObjectToSelectList(_istate.GetCountryList().Result);

                _statemodel.StateId = _state.StateId;
                _statemodel.StateName = _state.StateName;
                _statemodel.StateCode = _state.StateCode;
                _statemodel.CountryId = _state.CountryId;
                return View(_statemodel);
            }
            catch (Exception ex)
            {
                return Json("EditState " + ex.Message);
            }
        }

        public ActionResult DeleteState(int id)
        {
            try
            {
                _istate.Delete(id);
                return RedirectToAction("StateList");
            }
            catch (Exception ex)
            {
                return Json("DeleteState " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateState(StateModel statem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _istate.CheckDuplicate(statem.StateName).Result;
                    if (duplicate == 0)
                    {
                        StateDetails _state = new StateDetails();
                        _state.EntryDate = DateTime.Now.Date;
                        _state.SessionYear = Helper.Create_Session();
                        _state.UserID = HttpContext.Session.GetInt32("userid").Value;
                        _state.StateId = _istate.GetNewStateId().Result;
                        _state.StateName = statem.StateName;
                        _state.StateCode = statem.StateCode;
                        _state.CountryId = statem.CountryId;

                        string result = _istate.Post(_state).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(statem);
            }
            catch (Exception ex)
            {
                return Json("CreateState " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditState")]
        public IActionResult EditState(StateModel statem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    StateDetails _state = new StateDetails();
                    _state.StateId = statem.StateId;
                    _state.StateName = statem.StateName;
                    _state.StateCode = statem.StateCode;
                    _state.CountryId = statem.CountryId;
                    string result = _istate.Put(_state).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(statem);
            }
            catch (Exception ex)
            {
                return Json("EditState " + ex.Message);
            }
        }

        public JsonResult RefreshCountry() 
        {
            try
            {
                return Json(_istate.GetCountryList().Result);
            }
            catch (Exception ex)
            {
                return Json("RefreshCountry " + ex.Message);
            }
        }

    }
}
