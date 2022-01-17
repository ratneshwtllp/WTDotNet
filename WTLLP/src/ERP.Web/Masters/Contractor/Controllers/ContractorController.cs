using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using ERP.Web.Areas.Contractor.Models;
using Microsoft.AspNetCore.Http;

namespace ERP.Web.Areas.Contractor.Controllers
{
    [AuthLogin]
    [Area("Contractor")]
    public class ContractorController : Controller
    {
        ContractorModel _contractormodel = new ContractorModel();
        IHostingEnvironment _env;
        IContractorContract _icontractor;

        public ContractorController(IHostingEnvironment env, IContractorContract contractor)
        {
            _env = env;
            _icontractor = contractor;
        }

        public ActionResult ContractorList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("ContractorList " + ex.Message);
            }
        }

        public ActionResult ContractorList_Partial(String search)
        {
            try
            {
                return PartialView("_ContractorList", _icontractor.GetContractorRecord(search).Result);
            }
            catch (Exception ex)
            {
                return Json("ContractorList_Partial " + ex.Message);
            }
        }

        public ActionResult CreateContractor()
        {
            try
            {
                _contractormodel = new ContractorModel();
                _contractormodel.CompanyList = Helper.ConvertObjectToSelectList(_icontractor.GetCompanyList().Result);
                _contractormodel.ActiveDeactiveList = Helper.FillDropDownList_ActiveDeactiveWithValue();

                _contractormodel.OpeningDate = DateTime.Now.Date;
                return View(_contractormodel);
            }
            catch (Exception ex)
            {
                return Json("CreateContractor " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateContractor(ContractorModel ContractorM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _icontractor.CheckDuplicate(ContractorM.ContractorName).Result;
                    if (duplicate == 0)
                    {
                        ContractorDetails _contractordetails = new ContractorDetails();

                        _contractordetails.EntryDate = DateTime.Now.Date;
                        _contractordetails.SessionYear = Helper.Create_Session();
                        _contractordetails.UserID = HttpContext.Session.GetInt32("userid").Value;
                        _contractordetails.ContractorID = _icontractor.GetNewContractorId().Result;
                        _contractordetails.ContractorName = ContractorM.ContractorName;
                        _contractordetails.CompanyID = ContractorM.CompanyID;
                        _contractordetails.Phone1 = ContractorM.Phone1;
                        _contractordetails.Mobile1 = ContractorM.Mobile1;
                        _contractordetails.Address = ContractorM.Address;
                        _contractordetails.OpeningBalance = ContractorM.OpeningBalance;
                        _contractordetails.OpeningDate = ContractorM.OpeningDate;
                        _contractordetails.IsPayable = ContractorM.IsPayable;
                        _contractordetails.Active_Deactive = ContractorM.Active_Deactive;
                        string result = _icontractor.Post(_contractordetails).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(ContractorM);
            }
            catch (Exception ex)
            {
                return Json("CreateContractor " + ex.Message);
            }
        }

        public ActionResult EditContractor(int id)
        {
            try
            {
                ViewContractorDetails _vcontractordetails = new ViewContractorDetails();
                _vcontractordetails = _icontractor.GetContractorRecord(id).Result;

                _contractormodel = new ContractorModel();
                _contractormodel.CompanyList = Helper.ConvertObjectToSelectList(_icontractor.GetCompanyList().Result);
                _contractormodel.ActiveDeactiveList = Helper.FillDropDownList_ActiveDeactiveWithValue();

                _contractormodel.ContractorID = _vcontractordetails.ContractorID;
                _contractormodel.ContractorName = _vcontractordetails.ContractorName;
                _contractormodel.Phone1 = _vcontractordetails.Phone1;
                _contractormodel.Mobile1 = _vcontractordetails.Mobile1;
                _contractormodel.Address = _vcontractordetails.Address;
                _contractormodel.OpeningBalance = _vcontractordetails.OpeningBalance;
                _contractormodel.OpeningDate = _vcontractordetails.OpeningDate;
                _contractormodel.CompanyID = _vcontractordetails.ID;
                _contractormodel.IsPayable = _vcontractordetails.IsPayable;
                _contractormodel.Active_Deactive = _vcontractordetails.Active_Deactive;
                return View(_contractormodel);
            }
            catch (Exception ex)
            {
                return Json("EditContractor " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditContractor")]
        public IActionResult EditContractor(ContractorModel ContractorM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContractorDetails _contractordetails = new ContractorDetails();
                    _contractordetails.ContractorID = ContractorM.ContractorID;
                    _contractordetails.ContractorName = ContractorM.ContractorName;
                    _contractordetails.CompanyID = ContractorM.CompanyID;
                    _contractordetails.Phone1 = ContractorM.Phone1;
                    _contractordetails.Mobile1 = ContractorM.Mobile1;
                    _contractordetails.Address = ContractorM.Address;
                    _contractordetails.OpeningBalance = ContractorM.OpeningBalance;
                    _contractordetails.OpeningDate = ContractorM.OpeningDate;
                    _contractordetails.IsPayable = ContractorM.IsPayable;
                    _contractordetails.Active_Deactive = ContractorM.Active_Deactive;

                    string result = _icontractor.Put(_contractordetails).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(ContractorM);
            }
            catch (Exception ex)
            {
                return Json("EditContractor " + ex.Message);
            }
        }

        public ActionResult DeleteContractor(long contractorid)
        {
            try
            {
                _icontractor.Delete(contractorid);
                return RedirectToAction("ContractorList");
            }
            catch (Exception ex)
            {
                return Json("DeleteContractor " + ex.Message);
            }
        }
    }
}
