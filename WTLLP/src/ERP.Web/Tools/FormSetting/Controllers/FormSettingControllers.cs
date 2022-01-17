using ERP.Business.Contracts;
using ERP.Domain.Entities;
using ERP.Web.Areas.FormSetting.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using FastMember;
using CrystalDecisions.Shared;

namespace ERP.Web.Areas.FormSetting.Controllers
{
    [AuthLogin]
    [Area("FormSetting")]
    public class FormSettingController : Controller
    {
        FormSettingModel FRM = new FormSettingModel();
        IHostingEnvironment _env;
        IFormSettingContract _iformSetting;
        public FormSettingController(IHostingEnvironment env, IFormSettingContract IFSC)
        {
            _env = env;
            _iformSetting = IFSC;
        }

        public ActionResult FormSetting()
        {
            try
            {
                List<FormNumberSettingsDetails> _form = new List<FormNumberSettingsDetails>();
                _form = _iformSetting.Get().Result;

                List<FormSettingModel> icollection = new List<FormSettingModel>();
                FormSettingModel FRM;
                for (int i = 0; i < _form.Count; i++)
                {
                    FRM = new FormSettingModel();
                    FRM.SNo = _form[i].SNo;
                    FRM.FormName = _form[i].FormName;
                    FRM.Prefix = _form[i].Prefix;
                    FRM.SessionYear = _form[i].SessionYear;
                    FRM.StartingNumber = _form[i].StartingNumber;
                    FRM.NoOfDigits = _form[i].NoOfDigits;
                    FRM.DispalyNumber = _form[i].DispalyNumber;
                    FRM.EntryDate = _form[i].EntryDate;
                    FRM.IsBatchOfRM = _form[i].IsBatchOfRM;
                    lock (icollection)
                    {
                        icollection.Add(FRM);
                    }
                }
                return View(icollection);
            }
            catch (Exception ex)
            {
                return Json("FormSetting " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult SaveFormSetting([FromBody]FormSettingModel FSM)
        {
            try
            {
                FormNumberSettingsDetails _Form = new FormNumberSettingsDetails();
                _Form.SNo = FSM.SNo;
                _Form.FormName = FSM.FormName;
                _Form.Prefix = FSM.Prefix;
                _Form.SessionYear = FSM.SessionYear;
                _Form.StartingNumber = FSM.StartingNumber;
                _Form.NoOfDigits = FSM.NoOfDigits;
                _Form.DispalyNumber = FSM.DispalyNumber;
                _Form.IsBatchOfRM = FSM.IsBatchOfRM;

                string result = _iformSetting.Put(_Form).Result;
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json("SaveFormSetting " + ex.Message);
            }
        }

    }
}

