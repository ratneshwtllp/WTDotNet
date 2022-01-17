using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using ERP.Web.Areas.Supplier.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Data;
using FastMember;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ERP.Web.Areas.Supplier.Controllers
{
    [AuthLogin]
    [Area("Supplier")]
    public class SupplierController : Controller
    {
        SupplierModel _suppliermodel = new SupplierModel();
        IHostingEnvironment _env;
        ISupplierContract _isupplier;

        public SupplierController(IHostingEnvironment env, ISupplierContract supplier)
        {
            _env = env;
            _isupplier = supplier;
        }
        
        public ActionResult SupplierList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("SupplierList " + ex.Message);
            }
        }

        public ActionResult SupplierList_Partial(String search)
        {
            try
            {
                return PartialView("_SupplierList", _isupplier.GetSupplierList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("SupplierList_Partial " + ex.Message);
            }
        }

        [HttpGet]
        public JsonResult GetStateList(int id)
        {
            try
            {
                return Json(_isupplier.GetStateList(id).Result);
            }
            catch (Exception ex)
            {
                return Json("GetStateList " + ex.Message);
            }
        }

        [HttpGet]
        public JsonResult GetCityList(int id)
        {
            try
            {
                return Json(_isupplier.GetCityList(id).Result);
            }
            catch (Exception ex)
            {
                return Json("GetCityList " + ex.Message);
            }
        }

        public ActionResult CreateSupplier()
        {
            try
            {
                _suppliermodel = new SupplierModel();
                _suppliermodel.CountryList = Helper.ConvertObjectToSelectList(_isupplier.GetCountryList().Result);
                _suppliermodel.StateList = Helper.FillDropDownList_Empty();
                _suppliermodel.CityList = Helper.FillDropDownList_Empty();
                _suppliermodel.CurrencyList = Helper.ConvertObjectToSelectList(_isupplier.GetCurrencyList().Result);
                //_suppliermodel.StateList = Helper.ConvertObjectToSelectList(_isupplier.GetStateList().Result);
                //_suppliermodel.CityList = Helper.ConvertObjectToSelectList(_isupplier.GetCityList().Result);

                _suppliermodel.SupplierCode = _isupplier.GetNewSupplierId().Result.ToString();
                return View(_suppliermodel);
            }
            catch (Exception ex)
            {
                return Json("CreateSupplier " + ex.Message);
            }
        }

        public ActionResult EditSupplier(int id)
        {
            try
            {
                SupplierDetails _supplier = new SupplierDetails();
                _supplier = _isupplier.Get(id).Result;

                _suppliermodel = new SupplierModel();
                _suppliermodel.CountryList = Helper.ConvertObjectToSelectList(_isupplier.GetCountryList().Result);
                _suppliermodel.StateList = Helper.ConvertObjectToSelectList(_isupplier.GetStateList(_supplier.CountryId).Result);
                _suppliermodel.CityList = Helper.ConvertObjectToSelectList(_isupplier.GetCityList(_supplier.StateId).Result);
                _suppliermodel.CurrencyList = Helper.ConvertObjectToSelectList(_isupplier.GetCurrencyList().Result);

                _suppliermodel.SupplierId = _supplier.SupplierId;
                _suppliermodel.SupplierName = _supplier.SupplierName;
                _suppliermodel.SupplierCode = _supplier.SupplierCode;
                _suppliermodel.ContactPerson = _supplier.ContactPerson;
                _suppliermodel.SupplierAddress = _supplier.SupplierAddress;
                _suppliermodel.CountryId = _supplier.CountryId;
                _suppliermodel.StateId = _supplier.StateId;
                _suppliermodel.CityId = _supplier.CityId;
                _suppliermodel.Pincode = _supplier.Pincode;

                _suppliermodel.SupplierPhoneNo = _supplier.SupplierPhoneNo;
                _suppliermodel.SupplierMobileNo = _supplier.SupplierMobileNo;
                _suppliermodel.SupplierFaxNo = _supplier.SupplierFaxNo;
                _suppliermodel.SupplierEmail = _supplier.SupplierEmail;
                _suppliermodel.SupplierWeb = _supplier.SupplierWeb;
                _suppliermodel.SupplierPANNo = _supplier.SupplierPANNo;
                _suppliermodel.SupplierCSTNo = _supplier.SupplierCSTNo;
                _suppliermodel.SupplierUPTTNo = _supplier.SupplierUPTTNo;
                _suppliermodel.SupplierTINNo = _supplier.SupplierTINNo;
                _suppliermodel.SupplierLocalTAX = _supplier.SupplierLocalTAX;

                _suppliermodel.SupplierTerms = _supplier.SupplierTerms;
                _suppliermodel.SupplierDesc = _supplier.SupplierDesc;
                _suppliermodel.PType = _supplier.PType;
                _suppliermodel.BlackListed = _supplier.BlackListed;
                _suppliermodel.CID = _supplier.CurrencyID;
                _suppliermodel.GSTN = _supplier.GSTN;

                return View(_suppliermodel);
            }
            catch (Exception ex)
            {
                return Json("EditSupplier " + ex.Message);
            }
        }

        public ActionResult DeleteSupplier(int id)
        {
            try
            {
                _isupplier.Delete(id);
                return RedirectToAction("SupplierList");
            }
            catch (Exception ex)
            {
                return Json("DeleteSupplier " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateSupplier(SupplierModel suppm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _isupplier.CheckDuplicate(suppm.SupplierName).Result;
                    if (duplicate == 0)
                    {
                        SupplierDetails _supplier = new SupplierDetails();
                        _supplier.EntryDate = DateTime.Now.Date;
                        _supplier.SessionYear = Helper.Create_Session();
                        _supplier.UserId = HttpContext.Session.GetInt32("userid").Value;
                        _supplier.SupplierId = _isupplier.GetNewSupplierId().Result;
                        _supplier.SupplierName = suppm.SupplierName;

                        _supplier.SupplierCode = suppm.SupplierCode;
                        _supplier.ContactPerson = suppm.ContactPerson;
                        _supplier.SupplierAddress = suppm.SupplierAddress;
                        _supplier.CountryId = suppm.CountryId;
                        _supplier.StateId = suppm.StateId;
                        _supplier.CityId = suppm.CityId;
                        _supplier.Pincode = suppm.Pincode;

                        _supplier.SupplierPhoneNo = suppm.SupplierPhoneNo;
                        _supplier.SupplierMobileNo = suppm.SupplierMobileNo;
                        _supplier.SupplierFaxNo = suppm.SupplierFaxNo;
                        if (suppm.SupplierEmail == null)
                        {
                            _supplier.SupplierEmail = "-";
                        }
                        else
                        {
                            _supplier.SupplierEmail = suppm.SupplierEmail;
                        }
                        _supplier.SupplierWeb = suppm.SupplierWeb;
                        _supplier.SupplierPANNo = suppm.SupplierPANNo;
                        _supplier.SupplierCSTNo = suppm.SupplierCSTNo;
                        _supplier.SupplierUPTTNo = suppm.SupplierUPTTNo;
                        _supplier.SupplierTINNo = suppm.SupplierTINNo;
                        _supplier.SupplierLocalTAX = suppm.SupplierLocalTAX;

                        _supplier.SupplierTerms = suppm.SupplierTerms;
                        _supplier.SupplierDesc = suppm.SupplierDesc;
                        _supplier.PType = 0;
                        _supplier.BlackListed = 0;
                        _supplier.CurrencyID = suppm.CID;
                        _supplier.GSTN = suppm.GSTN;

                        string result = _isupplier.Post(_supplier).Result;
                        ModelState.Clear();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("1");
                    }
                }
                return View(suppm);
            }
            catch (Exception ex)
            {
                return Json("CreateSupplier " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditSupplier")]
        public IActionResult EditSupplier(SupplierModel suppm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SupplierDetails _supplier = new SupplierDetails();
                    _supplier.SupplierId = suppm.SupplierId;
                    _supplier.SupplierName = suppm.SupplierName;

                    _supplier.SupplierCode = suppm.SupplierCode;
                    _supplier.ContactPerson = suppm.ContactPerson;
                    _supplier.SupplierAddress = suppm.SupplierAddress;
                    _supplier.CountryId = suppm.CountryId;
                    _supplier.StateId = suppm.StateId;
                    _supplier.CityId = suppm.CityId;
                    _supplier.Pincode = suppm.Pincode;

                    _supplier.SupplierPhoneNo = suppm.SupplierPhoneNo;
                    _supplier.SupplierMobileNo = suppm.SupplierMobileNo;
                    _supplier.SupplierFaxNo = suppm.SupplierFaxNo;
                    _supplier.SupplierEmail = suppm.SupplierEmail;
                    _supplier.SupplierWeb = suppm.SupplierWeb;
                    _supplier.SupplierPANNo = suppm.SupplierPANNo;
                    _supplier.SupplierCSTNo = suppm.SupplierCSTNo;
                    _supplier.SupplierUPTTNo = suppm.SupplierUPTTNo;
                    _supplier.SupplierTINNo = suppm.SupplierTINNo;
                    _supplier.SupplierLocalTAX = suppm.SupplierLocalTAX;

                    _supplier.SupplierTerms = suppm.SupplierTerms;
                    _supplier.SupplierDesc = suppm.SupplierDesc;
                    _supplier.PType = 0;
                    _supplier.BlackListed = 0;
                    _supplier.CurrencyID = suppm.CID;
                    _supplier.GSTN = suppm.GSTN;

                    string result = _isupplier.Put(_supplier).Result;
                    ModelState.Clear();
                    return Ok(result);
                }
                return View(suppm);
            }
            catch (Exception ex)
            {
                return Json("EditSupplier " + ex.Message);
            }
        }

        [HttpGet]
        public ActionResult SupplierBank(long id)
        {
            try
            {
                SupplierDetails _supplierdetails = new SupplierDetails();
                _supplierdetails = _isupplier.Get(Convert.ToInt32(id)).Result;
                List<ViewSupplierBank> _list = new List<ViewSupplierBank>();
                _list = _isupplier.GetSupplierBank(id).Result;
                SupplierBankModel _supplierbankmodel;
                List<SupplierBankModel> _listmodel = new List<SupplierBankModel>();
                for (int i = 0; i < _list.Count; i++)
                {
                    _supplierbankmodel = new SupplierBankModel();
                    _supplierbankmodel.SupplierBankId = _list[i].SupplierBankId;
                    _supplierbankmodel.SupplierId = _list[i].SupplierId;
                    _supplierbankmodel.Beneficiary = _list[i].Beneficiary;
                    _supplierbankmodel.BankName = _list[i].BankName;
                    _supplierbankmodel.BankAddress = _list[i].BankAddress;
                    _supplierbankmodel.BankSwiftCode = _list[i].BankSwiftCode;
                    _supplierbankmodel.BankIFSCCode = _list[i].BankIFSCCode;
                    _supplierbankmodel.AccountNo = _list[i].AccountNo;
                    _listmodel.Add(_supplierbankmodel);
                }

                SupplierModel _suppliermodel = new SupplierModel();
                _suppliermodel.SupplierId = id;
                _suppliermodel.SupplierCode = _supplierdetails.SupplierCode;
                _suppliermodel.SupplierName = _supplierdetails.SupplierName;
                _suppliermodel.SupplierAddress = _supplierdetails.SupplierAddress;
                _suppliermodel.SupplierBankModel = _listmodel;
                _suppliermodel.BankList = Helper.ConvertObjectToSelectList(_isupplier.GetBankNameList().Result);
                return PartialView("SupplierBank", _suppliermodel);
            }
            catch (Exception ex)
            {
                return Json("Supplier Bank " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult SupplierBank(SupplierModel _suppliermodel)
        {
            try
            {
                SupplierBankDetails _supplierbank = new SupplierBankDetails();
                if (_suppliermodel.AccountNo == _suppliermodel.VerifyAccountNo)
                {
                    _supplierbank.SupplierBankId = _isupplier.GetNewSupplierBankId().Result;
                    _supplierbank.SupplierId = _suppliermodel.SupplierId;
                    _supplierbank.Beneficiary = _suppliermodel.Beneficiary;
                    _supplierbank.BankId = _suppliermodel.BankId;
                    _supplierbank.BankAddress = _suppliermodel.BankAddress;
                    _supplierbank.AccountNo = _suppliermodel.AccountNo;
                    _supplierbank.BankSwiftCode = _suppliermodel.BankSwiftCode;
                    _supplierbank.BankIFSCCode = _suppliermodel.BankIFSCCode;
                    _supplierbank.EntryDate = DateTime.Now.Date;
                    _supplierbank.UserId = HttpContext.Session.GetInt32("userid").Value;
                    _supplierbank.SessionYear = Helper.Create_Session();
                    string result = _isupplier.PostSupplierBank(_supplierbank).Result;
                }
                return RedirectToAction("SupplierBank", "Supplier", new RouteValueDictionary(new { Id = _suppliermodel.SupplierId }));
            }
            catch (Exception ex)
            {
                return Json("Supplier Bank" + ex.Message);
            }
        }
        public ActionResult DeleteSupplierBank(int supplierbankid, long supplierid)
        {
            try
            {
                _isupplier.DeleteSupplierBank(supplierbankid);
                return RedirectToAction("SupplierBank", "Supplier", new RouteValueDictionary(new { Id = supplierid }));
            }
            catch (Exception ex)
            {
                return Json("DeleteSupplierBank " + ex.Message);
            }
        }

        public ActionResult SupplierPrint(long id)
        {
            try
            {
                IEnumerable<ViewSupplierDetails> data = _isupplier.GetSupplierForPrint(id).Result;
                DataTable table = new DataTable();
                table.TableName = "ViewSupplierDetails";
                using (var reader = ObjectReader.Create(data))
                {
                    table.Load(reader);
                }

                table.WriteXml(Path.Combine(_env.WebRootPath, "crystal/xml/ViewSupplierDetails.xml"), XmlWriteMode.WriteSchema);
                //supplier Bank Sub Report
                IEnumerable<ViewSupplierBank> datachild = _isupplier.GetSupplierBank(Convert.ToInt32(id)).Result;
                DataTable tablechild = new DataTable();
                tablechild.TableName = "ViewSupplierBank";
                using (var reader = ObjectReader.Create(datachild))
                {
                    tablechild.Load(reader);
                }
                tablechild.WriteXml(Path.Combine(_env.WebRootPath, "crystal/xml/ViewSupplierBank.xml"), XmlWriteMode.WriteSchema);
                ReportClass rptH = new ReportClass();
                rptH.FileName = Path.Combine(_env.WebRootPath, "crystal/rpt/SupplierProfile.rpt");
                rptH.Load();
                rptH.SetDataSource(table);
                rptH.Subreports[0].SetDataSource(tablechild);
                Stream stream = rptH.ExportToStream(ExportFormatType.PortableDocFormat);
                return File(stream, "application/pdf");
            }
            catch (Exception ex)
            {
                return Json("SupplierPrint " + ex.Message);
            }
        }
        
        public JsonResult RefreshCountry()
        {
            try
            {
                return Json(_isupplier.GetCountryList().Result);
            }
            catch (Exception ex)
            {
                return Json("RefreshCountry " + ex.Message);
            }
        }
        
        public JsonResult GetCurrencyList() 
        {
            try
            {
                return Json(_isupplier.GetCurrencyList().Result);
            }
            catch (Exception ex)
            {
                return Json("GetCurrencyList " + ex.Message);
            }
        }

        public JsonResult GetBankList() 
        {
            try
            {
                return Json(_isupplier.GetBankNameList().Result);
            }
            catch (Exception ex)
            {
                return Json("GetCurrencyList " + ex.Message);
            }
        }
    }
}
