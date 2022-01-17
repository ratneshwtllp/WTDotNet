using CrystalDecisions.CrystalReports.Engine;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using ERP.Web.Areas.RMProperty.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ERP.Web.Areas.RMProperty.Controllers
{
    [AuthLogin]
    [Area("RMProperty")]
    public class RMPropertiesController : Controller
    {
        RMPropertiesModel _rmpropertiesModel = new RMPropertiesModel();
        IHostingEnvironment _env;
        IRMPropertiesContract _irproperties;
        RMProperties _rmp = new RMProperties();
        public RMPropertiesController(IHostingEnvironment env, IRMPropertiesContract RProperties)
        {
            _env = env;
            _irproperties = RProperties;
        }


        // RM Properties

        [HttpGet]
        public ActionResult CreateRMP()
        {
            try
            {
                _rmpropertiesModel = new RMPropertiesModel();
                _rmpropertiesModel.IsPrintOnPoList = Helper.FillDropDownList_YesNoWithID();
                _rmpropertiesModel.IsMasterList = Helper.FillDropDownList_YesNoWithID();
                _rmpropertiesModel.RMPropertiesIsPrintOnPo = 1;
                _rmpropertiesModel.RMPropertiesIsMaster = 1;
                return View("CreateRMP", _rmpropertiesModel);
            }
            catch (Exception ex)
            {
                return Json("CreateRMP " + ex.Message);
            }
        }

        public ActionResult RMPList()
        {
            try
            {
                return View("RMPList", _rmpropertiesModel);
            }
            catch (Exception ex)
            {
                return Json("RMPList " + ex.Message);
            }
        }

        public ActionResult RMP_Partial(String  search)
        {
            try
            {
                return PartialView("_RMPList", _irproperties.RMPropertiesList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("RMP_Partial " + ex.Message);
            }
        }
        [HttpPost, ActionName("SaveRMP")]
        public JsonResult SaveRMP([FromBody]RMPropertiesModel rmpmodel)
        {
            _rmp = new RMProperties();
            string result;
            //_RMPRO.RMPropertiesID = RMPRO.RMPropertiesID;
            _rmp.RMPropertiesName = rmpmodel.RMPropertiesName;
            _rmp.RMPropertiesRemark = rmpmodel.RMPropertiesRemark;
            _rmp.RMPropertiesIsPrintOnPo = rmpmodel.RMPropertiesIsPrintOnPo;
            _rmp.RMPropertiesIsMaster = rmpmodel.RMPropertiesIsMaster;
            _rmp.EntryDate = DateTime.Now.Date;
            _rmp.SessionYear = Helper.Create_Session();
            _rmp.UserId = HttpContext.Session.GetInt32("userid").Value;
            if (rmpmodel.EditMode == true)
            {
                _rmp.RMPropertiesID = rmpmodel.RMPropertiesID;
                result = _irproperties.Put(_rmp).Result;
            }
            else 
            {
                rmpmodel.RMPropertiesID = _irproperties.GetNewRMPId().Result;
                _rmp.RMPropertiesID = rmpmodel.RMPropertiesID;
                result = _irproperties.Post(_rmp).Result;
            }
            return Json(result);
        }

        public ActionResult EditRMP(long id)
        {
             _rmp = new RMProperties();
            _rmp = _irproperties.GetValue(id).Result;
            _rmpropertiesModel.IsPrintOnPoList = Helper.FillDropDownList_YesNoWithID();
            _rmpropertiesModel.IsMasterList = Helper.FillDropDownList_YesNoWithID();

            _rmpropertiesModel.RMPropertiesName = _rmp.RMPropertiesName;
            _rmpropertiesModel.RMPropertiesIsPrintOnPo = _rmp.RMPropertiesIsPrintOnPo;
            _rmpropertiesModel.RMPropertiesIsMaster = _rmp.RMPropertiesIsMaster;
            _rmpropertiesModel.RMPropertiesRemark = _rmp.RMPropertiesRemark;
            _rmpropertiesModel.EditMode = true;
            _rmpropertiesModel.RMPropertiesID = _rmp.RMPropertiesID;
            return View("CreateRMP", _rmpropertiesModel);
        }

        public ActionResult DeleteRMP(int id)
        {
            try
            {
               var result= _irproperties.Delete(id).Result ;
                return Json (result);
            }
            catch (Exception ex)
            {
                return Json("DeleteRMP" + ex.Message);
            }
        }

        //*******************************




        // For RM Properties Value
        [HttpGet]
        public ActionResult CreateRMPValue()
        {
            try
            {
                RMPValueModel _rmpvaluemodel = new RMPValueModel();
                _rmpvaluemodel.RMPropertiesValueList = Helper.ConvertObjectToSelectList(_irproperties.GetRMPValueList().Result);
                return View("CreateRMPValue", _rmpvaluemodel);
            }
            catch (Exception ex)
            {
                return Json("CreateRMPValue " + ex.Message);
            }
        }

        [HttpPost, ActionName("SaveRMPValue")]
        public JsonResult SaveRMPValue([FromBody]RMPValueModel rmpvaluemodel)
        {
            RMPropertiesValue rmpvalue = new RMPropertiesValue();
       
            //_RMPRO.RMPropertiesID = RMPRO.RMPropertiesID;
            rmpvalue.RMPropertiesID = rmpvaluemodel.RMPropertiesValId;
            rmpvalue.RMPropertiesValueName = rmpvaluemodel.RMPropertiesValue;
            rmpvalue.RMPropertiesValueRemark = rmpvaluemodel.RMPropertiesRemark;
            rmpvalue.EntryDate = DateTime.Now.Date;
            rmpvalue.SessionYear = Helper.Create_Session();
            rmpvalue.UserId = HttpContext.Session.GetInt32("userid").Value;
            string result="";
            if (rmpvaluemodel.EditMode == true)
            {
                rmpvalue.RMPropertiesValueID = rmpvaluemodel.RMPropertiesValueID;
                result = _irproperties.PutValue(rmpvalue).Result;
            }
            else
            {
                rmpvaluemodel.RMPropertiesValueID = _irproperties.GetNewRMPValueId().Result;
                rmpvalue.RMPropertiesValueID = rmpvaluemodel.RMPropertiesValueID;
                result = _irproperties.PostValue(rmpvalue).Result;
            }
            return Json(result);
        }
        [HttpGet]
        public ActionResult RMPValueList()
        {
            try
            {
                RMPValueModel rmpvaluemodel = new RMPValueModel();

              //  rmpvaluemodel.RMPropertiesValueList = Helper.ConvertObjectToSelectList(_irproperties.GetRMPValueList().Result);
                return View("RMPValueList",rmpvaluemodel);
            }
            catch (Exception ex)
            {
                return Json("RMPValueList " + ex.Message);
            }
        }

        public ActionResult RMPValue_Partial(String search)
        {
            try
            {
                return PartialView("_RMPValueList", _irproperties.RMProValueList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("RMPValue_Partial" + ex.Message);
            }
        }

        public ActionResult EditRMPValue(long id)
        {
            RMPValueModel rmpvaluemodel = new RMPValueModel();

            RMPropertiesValue rmppro = new RMPropertiesValue();
            rmppro = _irproperties.GetRMValue(id).Result;
            rmpvaluemodel.RMPropertiesValueList = Helper.ConvertObjectToSelectList(_irproperties.GetRMPValueList().Result);

            rmpvaluemodel.RMPropertiesValId = rmppro.RMPropertiesID;
            rmpvaluemodel.RMPropertiesValue = rmppro.RMPropertiesValueName;
            rmpvaluemodel.RMPropertiesRemark = rmppro.RMPropertiesValueRemark;
            rmpvaluemodel.EditMode = true;
            rmpvaluemodel.RMPropertiesValueID = rmppro.RMPropertiesValueID;
            return View("CreateRMPValue", rmpvaluemodel);
        }

        public ActionResult DeleteRMProValue(int id)
        {
            try
            {
                _irproperties.DeleteVal(id);
                return RedirectToAction("RMPValueList ");
            }
            catch (Exception ex)
            {
                return Json("DeleteRMPValue " + ex.Message);
            }
        }

        // For Mapping details
        public ActionResult CreateMapping()
        {
            try
            {
                MappingModel mappingmodel = new MappingModel();
                mappingmodel = new MappingModel();
                mappingmodel.RMCategoryList = Helper.ConvertObjectToSelectList(_irproperties.GetCategoryList().Result);
                return View("CreateMapping", mappingmodel);
            }
            catch (Exception ex)
            {
                return Json("CreateMapping " + ex.Message);
            }
        }

        [HttpPost, ActionName("CreateMapping")]
        public IActionResult CreateMapping(MappingModel mappingmodel)
        {
            try
            {
                RMPropertiesMapping rmpromapping = new RMPropertiesMapping();

                if (ModelState.IsValid)
                {
                    string result = "";
                    if (mappingmodel.RMPropertiesModel.Count > 0)
                    {
                        _irproperties.DeleteRMmapping(mappingmodel.CategoryID);
                        
                        for (int i = 0; i < mappingmodel.RMPropertiesModel.Count; i++)
                        {
                            if (mappingmodel.RMPropertiesModel[i].Selected == true)
                            {
                                rmpromapping.CategoryID = mappingmodel.CategoryID;
                                rmpromapping.RMPropertiesID = mappingmodel.RMPropertiesModel[i].RMPropertiesID;
                                rmpromapping.IsRequired = mappingmodel.RMPropertiesModel[i].IsRequired ;
                                result = _irproperties.PostMapping(rmpromapping).Result;
                            }
                        }

                    }
                    ModelState.Clear();
                    return Ok(result);
                }
                else
                {
                    return View(mappingmodel);
                }
            }
            catch (Exception ex)
            {
                return Json("CreateMapping " + ex.Message);
            }
        }

        public ActionResult RMPMappingList()
        {
            try
            {
                MappingModel mappingmodel = new MappingModel();
                mappingmodel.RMCategoryList = Helper.ConvertObjectToSelectList(_irproperties.GetCategoryList().Result);
                //return View();
                return View("RMPMappingList", mappingmodel);
            }
            catch (Exception ex)
            {
                return Json("RMPMappingList " + ex.Message);
            }
        }

        public ActionResult RMPMapingList_Partial(RMProSearch rms)
        {
            try
            {
                var data = _irproperties.RMMappingList(rms).Result;
                return PartialView("_RMPMappingList", data);
            }
            catch (Exception ex)
            {
                return Json("RMPMapingList_Partial " + ex.Message);
            }
        }

        public ActionResult DeleteRMPMapping(int id)
        {
            try
            {
                _irproperties.DeleteMapping(id);
                return RedirectToAction("RMPMappingList");
            }
            catch (Exception ex)
            {
                return Json("DeleteRMPMapping " + ex.Message);
            }
        }

        public ActionResult EditRMPMapp(int id)
        {
            try
            {

                MappingModel mappingmodel = new MappingModel();
                RMPropertiesMapping _rmpmapping = new RMPropertiesMapping();
                mappingmodel.RMCategoryList = Helper.ConvertObjectToSelectList(_irproperties.GetCategoryList().Result);
                mappingmodel.IsRequiredList = Helper.FillDropDownList_YesNoWithValue();
                List<RMProperties> RMP = _irproperties.GetRMproList().Result;

                List<RMPropertiesMapping> MappingList = _irproperties.GetMapList().Result;
                List<RMPropertiesMapping> existingrmmapping = _irproperties.GetRMMapProperties(id).Result;              

                bool Rmpro_Mapp = false;
                for (var i = 0; i < MappingList.Count; i++)
                {
                    Rmpro_Mapp = false;                  
                    mappingmodel.CategoryID = MappingList[i].CategoryID;
                    mappingmodel.RMPropertiesID = MappingList[i].RMPropertiesID;
                    mappingmodel.IsRequired  = MappingList[i].IsRequired ;
                    mappingmodel.Selected = false;
                    for (var j = 0; j < existingrmmapping.Count; j++)
                    {
                        if (existingrmmapping[j].RMPropertiesID == MappingList[i].RMPropertiesID)
                        {
                            Rmpro_Mapp = true;
                            break;
                        }
                    }
                    if (Rmpro_Mapp)               
                    mappingmodel.Selected = true;               
                }
                return View("CreateMapping", mappingmodel);
                //return RedirectToAction("CreateMapping");
            }    
            catch (Exception ex)
            {
                return Json("EditRMPMapp " + ex.Message);
            }
        }

        public ActionResult CheckedPropertiesList(long  id)
        {
            try
            {
                MappingModel mappingmodel = new MappingModel();
                List<RMPropertiesMapping> existingrmmapping = _irproperties.GetRMMapProperties(id).Result;
                //mappingmodel.IsRequiredList = Helper.FillDropDownList_YesNoWithValue();
                List<RMProperties> _rmproperties = _irproperties.GetRMproList().Result;
                List<RMPropertiesModel > rmpropertieslist = new List<RMPropertiesModel>();
                RMPropertiesModel  _rmp;
                bool findmapping = false;
                for (var i = 0; i < _rmproperties.Count; i++)
                {
                    findmapping = false;
                    _rmp = new RMPropertiesModel();
                    _rmp.RMPropertiesID  = _rmproperties[i].RMPropertiesID ;
                    _rmp.RMPropertiesName  = _rmproperties[i].RMPropertiesName;
                    _rmp.IsRequiredList = Helper.FillDropDownList_YesNoWithValue();
                    _rmp.Selected = false;
                    for (var j = 0; j < existingrmmapping.Count; j++)
                    {
                        _rmp.IsRequired = existingrmmapping[j].IsRequired;

                        if (existingrmmapping[j].RMPropertiesID  == _rmproperties[i].RMPropertiesID)
                        {
                            findmapping = true;
                            break;
                        }
                    }
                    if (findmapping)
                        _rmp.Selected = true;

                    lock (rmpropertieslist)
                    {
                        rmpropertieslist.Add(_rmp);
                    }
                  
                }
                mappingmodel.RMPropertiesModel = rmpropertieslist;
                return View("_RMPCheckedList", mappingmodel);
            }
            catch (Exception ex)
            {
                return Json("_RMPCheckedList " + ex.Message);
            }
        }

        public JsonResult RefresRMPList()
        {
            try
            {
                return Json(_irproperties.GetRMPValueList().Result);
            }
            catch (Exception ex)
            {
                return Json("CreateRMP" + ex.Message);
            }
        }
        public JsonResult CheckRMPValue(long id,string value,long rmpvalueid)
        {
            try
            {
                return Json(_irproperties.CheckRMPValue(id, value, rmpvalueid).Result);
            }
            catch (Exception ex)
            {
                return Json("CheckRMPValue " + ex.Message);
            }
        }
        
        public JsonResult CheckRMP(string value,long rmpid)
        {
            try
            {
                return Json(_irproperties.CheckRMP(value,rmpid).Result);
            }
            catch (Exception ex)
            {
                return Json("CheckRMP" + ex.Message);
            }
        }

        public JsonResult RefreshRMCategory() 
        {
            try
            {
                return Json(_irproperties.GetCategoryList().Result); 
            }
            catch (Exception ex)
            {
                return Json("RefreshRMCategory" + ex.Message); 
            }
        }

    }
}

