using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using ERP.Web.Areas.Buyer.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Routing;
using System.Data;
using CrystalDecisions.Shared;
using FastMember;
using CrystalDecisions.CrystalReports.Engine;

namespace ERP.Web.Areas.Buyer.Controllers
{
    [AuthLogin]
    [Area("Buyer")]
    public class BuyerController : Controller
    {
        BuyerModel _buyermodel = new BuyerModel();
        IHostingEnvironment _env;
        IBuyerContract _ibuyer;

        public BuyerController(IHostingEnvironment env, IBuyerContract buyer)
        {
            _env = env;
            _ibuyer = buyer;
        }

        public ActionResult BuyerList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("BuyerList " + ex.Message);
            }
        }

        public ActionResult BuyerList_Partial(String search)
        {
            try
            {
                return PartialView("_BuyerList", _ibuyer.GetBuyerList(search).Result);
            }
            catch (Exception ex)
            {
                return Json("BuyerList_Partial " + ex.Message);
            }
        }

        [HttpGet]
        public JsonResult GetStateList(int id)
        {
            try
            {
                return Json(_ibuyer.GetStateList(id).Result);
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
                return Json(_ibuyer.GetCityList(id).Result);
            }
            catch (Exception ex)
            {
                return Json("GetCityList " + ex.Message);
            }
        }

        public ActionResult CreateBuyer()
        {
            try
            {
                _buyermodel = new BuyerModel();
                _buyermodel.CountryList = Helper.ConvertObjectToSelectList(_ibuyer.GetCountryList().Result);
                _buyermodel.StateList = Helper.FillDropDownList_Empty();
                _buyermodel.CityList = Helper.FillDropDownList_Empty();
                _buyermodel.CurrencyList = Helper.ConvertObjectToSelectList(_ibuyer.GetCurrencyList().Result);
                //_suppliermodel.StateList = Helper.ConvertObjectToSelectList(_isupplier.GetStateList().Result);
                //_suppliermodel.CityList = Helper.ConvertObjectToSelectList(_isupplier.GetCityList().Result);
                _buyermodel.FOBList = Helper.FillDropDownList_FOBCIF();
                _buyermodel.IsActiveList = Helper.FillDropDownList_YesNoWithValue();
                _buyermodel.IsAllowLoginList = Helper.FillDropDownList_YesNoWithValue();
                _buyermodel.IsGeneralBuyerList = Helper.FillDropDownList_YesNoWithValue();

                _buyermodel.BankList = Helper.ConvertObjectToSelectList(_ibuyer.GetBankNameList().Result);
                List<ConsigneeDetails> s = _ibuyer.GetConsigneeName().Result;
                List<BuyerConsigneeModel> _buyerconModel = new List<BuyerConsigneeModel>();
                BuyerConsigneeModel _buyerconsignee;
                for (var i = 0; i < s.Count; i++)
                {
                    _buyerconsignee = new BuyerConsigneeModel();
                    _buyerconsignee.ConsigneeId = s[i].ConsigneeId;
                    _buyerconsignee.ConsigneeName = s[i].ConsigneeName;
                    _buyerconsignee.Selected = false;
                    _buyerconModel.Add(_buyerconsignee);
                }
                _buyermodel.BuyerConsigneeModel = _buyerconModel;
                _buyermodel.BuyerBankModel = new List<BuyerBankModel>();

                return View(_buyermodel);
            }
            catch (Exception ex)
            {
                return Json("CreateBuyer " + ex.Message);
            }
        }

        public ActionResult EditBuyer(int id)
        {
            try
            {
                BuyerDetails _buyer = new BuyerDetails();
                _buyer = _ibuyer.Get(id).Result;

                _buyermodel = new BuyerModel();
                _buyermodel.CountryList = Helper.ConvertObjectToSelectList(_ibuyer.GetCountryList().Result);
                _buyermodel.StateList = Helper.ConvertObjectToSelectList(_ibuyer.GetStateList(_buyer.CountryId).Result);
                _buyermodel.CityList = Helper.ConvertObjectToSelectList(_ibuyer.GetCityList(_buyer.StateId).Result);
                _buyermodel.CurrencyList = Helper.ConvertObjectToSelectList(_ibuyer.GetCurrencyList().Result);
                _buyermodel.FOBList = Helper.FillDropDownList_FOBCIF();
                _buyermodel.IsActiveList = Helper.FillDropDownList_YesNoWithValue();
                _buyermodel.IsAllowLoginList = Helper.FillDropDownList_YesNoWithValue();
                _buyermodel.IsGeneralBuyerList = Helper.FillDropDownList_YesNoWithValue();
                _buyermodel.BuyerId = _buyer.BuyerId;
                _buyermodel.BuyerName = _buyer.BuyerName;
                _buyermodel.BuyerCode = _buyer.BuyerCode;
                _buyermodel.ContactPerson = _buyer.ContactPerson;
                _buyermodel.BuyerAddress = _buyer.BuyerAddress;
                _buyermodel.CountryId = _buyer.CountryId;
                _buyermodel.StateId = _buyer.StateId;
                _buyermodel.CityId = _buyer.CityId;
                _buyermodel.Pincode = _buyer.Pincode;

                _buyermodel.BuyerPhoneNo = _buyer.BuyerPhoneNo;
                _buyermodel.BuyerMobileNo = _buyer.BuyerMobileNo;
                _buyermodel.BuyerFaxNo = _buyer.BuyerFaxNo;
                _buyermodel.BuyerEmail = _buyer.BuyerEmail;
                _buyermodel.BuyerWeb = _buyer.BuyerWeb;
                _buyermodel.BuyerPANNo = _buyer.BuyerPANNo;
                _buyermodel.BuyerCSTNo = _buyer.BuyerCSTNo;
                _buyermodel.BuyerUPTTNo = _buyer.BuyerUPTTNo;
                _buyermodel.BuyerTINNo = _buyer.BuyerTINNo;
                _buyermodel.BuyerLocalTAX = _buyer.BuyerLocalTAX;
                _buyermodel.BuyerTerms = _buyer.BuyerTerms;
                _buyermodel.BuyerDesc = _buyer.BuyerDesc;
                _buyermodel.PType = _buyer.PType;
                _buyermodel.BlackListed = _buyer.BlackListed;
                _buyermodel.CID = _buyer.CurrencyID;
                _buyermodel.BuyerUserID = _buyer.BuyerUserID;
                _buyermodel.BuyerPW = _buyer.BuyerPW;
                _buyermodel.FOB_CIF = _buyer.FOB_CIF;
                _buyermodel.IsActive = _buyer.IsActive;
                _buyermodel.IsAllowLogin = _buyer.IsAllowLogin;
                _buyermodel.IsGeneralBuyer = _buyer.IsGeneralBuyer;
                _buyermodel.LogoPath = _buyer.LogoPath;
             
                List<BuyerConsigneeDetails> existingconsignee = _ibuyer.GetBuyerConsignee(id).Result;
                List<ConsigneeDetails> consigneeList = _ibuyer.GetConsigneeList(id).Result;
                List<BuyerConsigneeModel> _buyerconsigneeModel = new List<BuyerConsigneeModel>();
                BuyerConsigneeModel _onesupplier;
                bool consignee_found = false;
                for (var i = 0; i < consigneeList.Count; i++)
                {
                    consignee_found = false;
                    _onesupplier = new BuyerConsigneeModel();
                    _onesupplier.ConsigneeId = consigneeList[i].ConsigneeId;
                    _onesupplier.ConsigneeName = consigneeList[i].ConsigneeName;
                    _onesupplier.Selected = false;
                    for (var j = 0; j < existingconsignee.Count; j++)
                    {
                        if (existingconsignee[j].ConsigneeId == consigneeList[i].ConsigneeId)
                        {
                            consignee_found = true;
                            break;
                        }
                    }
                    if (consignee_found)
                        _onesupplier.Selected = true;

                    _buyerconsigneeModel.Add(_onesupplier);
                }
                _buyermodel.BuyerConsigneeModel = _buyerconsigneeModel;
                // return View(_buyermodel);
                return View("CreateBuyer", _buyermodel);
            }
            catch (Exception ex)
            {
                return Json("EditBuyer " + ex.Message);
            }
        }

        public ActionResult DeleteBuyer(int id)
        {
            try
            {
                _ibuyer.Delete(id);
                return RedirectToAction("BuyerList");
            }
            catch (Exception ex)
            {
                return Json("DeleteBuyer " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBuyer(BuyerModel buyerm, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int duplicate = 0;
                    duplicate = _ibuyer.CheckDuplicate(buyerm.BuyerName).Result;
                    if (duplicate == 0)
                    {
                        BuyerDetails _buyer = new BuyerDetails();
                        _buyer.EntryDate = DateTime.Now.Date;
                        _buyer.SessionYear = Helper.Create_Session();
                        _buyer.UserId = HttpContext.Session.GetInt32("userid").Value;
                        _buyer.BuyerId = _ibuyer.GetNewBuyerId().Result;
                        _buyer.BuyerName = buyerm.BuyerName;

                        _buyer.BuyerCode = buyerm.BuyerCode;
                        _buyer.ContactPerson = buyerm.ContactPerson;
                        _buyer.BuyerAddress = buyerm.BuyerAddress;
                        _buyer.CountryId = buyerm.CountryId;
                        _buyer.StateId = buyerm.StateId;
                        _buyer.CityId = buyerm.CityId;
                        _buyer.Pincode = buyerm.Pincode;

                        _buyer.BuyerPhoneNo = buyerm.BuyerPhoneNo;
                        _buyer.BuyerMobileNo = buyerm.BuyerMobileNo;
                        _buyer.BuyerFaxNo = buyerm.BuyerFaxNo;
                        _buyer.BuyerEmail = buyerm.BuyerEmail;
                        _buyer.BuyerWeb = buyerm.BuyerWeb;
                        _buyer.BuyerPANNo = buyerm.BuyerPANNo;
                        _buyer.BuyerCSTNo = buyerm.BuyerCSTNo;
                        _buyer.BuyerUPTTNo = buyerm.BuyerUPTTNo;
                        _buyer.BuyerTINNo = buyerm.BuyerTINNo;
                        _buyer.BuyerLocalTAX = buyerm.BuyerLocalTAX;

                        _buyer.BuyerTerms = buyerm.BuyerTerms;
                        _buyer.BuyerDesc = buyerm.BuyerDesc;
                        _buyer.PType = 1;
                        _buyer.BlackListed = 0;
                        _buyer.CurrencyID = buyerm.CID;
                        _buyer.BuyerUserID = buyerm.BuyerEmail;//buyerm.BuyerUserID;
                        _buyer.BuyerPW = buyerm.BuyerPW;
                        _buyer.FOB_CIF = buyerm.FOB_CIF;
                        _buyer.LogoPath = buyerm.LogoPath;
                        if (file != null)  //for buyer image
                        {
                            var webRoot = _env.WebRootPath;
                            string buyerimagefilename = file.FileName;
                            int pos = file.FileName.LastIndexOf(".");
                            buyerimagefilename = file.FileName.Substring(0, pos);
                            buyerimagefilename = file.FileName.Replace(buyerimagefilename, _buyer.BuyerId.ToString());
                            var uploads = Path.Combine("uploads/buyerimage", buyerimagefilename);
                            var path = Path.Combine(_env.WebRootPath, uploads);
                            using (var fileStream = new FileStream(path, FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }
                            _buyer.LogoPath = uploads;
                        }
                        //else
                        //{
                        //    _buyer.LogoPath = @"uploads/catbgimage/catbgimage.jpg";
                        //}
                        _buyer.IsActive = buyerm.IsActive;
                        _buyer.IsAllowLogin = buyerm.IsAllowLogin;
                        _buyer.IsGeneralBuyer = buyerm.IsGeneralBuyer;
                        _buyer.Online_Transfer = 0;

                        List<BuyerConsigneeDetails> icollection = new List<BuyerConsigneeDetails>();

                        BuyerConsigneeDetails _buyerconsignee;
                        long buyerconsigneeid = _ibuyer.GetNewBuyerConsigneeId().Result;
                        for (int i = 0; i < buyerm.BuyerConsigneeModel.Count; i++)
                        {
                            if (buyerm.BuyerConsigneeModel[i].Selected == true)
                            {
                                _buyerconsignee = new BuyerConsigneeDetails();
                                _buyerconsignee.BuyerConsigneeId = buyerconsigneeid;
                                _buyerconsignee.BuyerId = _buyer.BuyerId;
                                _buyerconsignee.ConsigneeId = buyerm.BuyerConsigneeModel[i].ConsigneeId;
                                _buyerconsignee.EntryDate = DateTime.Now.Date;
                                _buyerconsignee.SessionYear = Helper.Create_Session();
                                _buyerconsignee.UserId = HttpContext.Session.GetInt32("userid").Value;
                                buyerconsigneeid++;
                                icollection.Add(_buyerconsignee);
                            }
                        }

                        _buyer.BuyerConsigneeDetails = icollection;
                        string result = _ibuyer.Post(_buyer).Result;
                        ModelState.Clear();
                        //return Ok(result);
                    }
                    //else
                    //{
                    //    return BadRequest("1");
                    //}
                    return RedirectToAction("CreateBuyer");
                }
                return View(buyerm);
            }
            catch (Exception ex)
            {
                return Json("CreateBuyer " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditBuyer")]
        public async Task<IActionResult> EditBuyer(BuyerModel buyerm, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BuyerDetails _buyer = new BuyerDetails();
                    _buyer.BuyerId = buyerm.BuyerId;
                    _buyer.BuyerName = buyerm.BuyerName;
                    _buyer.BuyerCode = buyerm.BuyerCode;
                    _buyer.ContactPerson = buyerm.ContactPerson;
                    _buyer.BuyerAddress = buyerm.BuyerAddress;
                    _buyer.CountryId = buyerm.CountryId;
                    _buyer.StateId = buyerm.StateId;
                    _buyer.CityId = buyerm.CityId;
                    _buyer.Pincode = buyerm.Pincode;

                    _buyer.BuyerPhoneNo = buyerm.BuyerPhoneNo;
                    _buyer.BuyerMobileNo = buyerm.BuyerMobileNo;
                    _buyer.BuyerFaxNo = buyerm.BuyerFaxNo;
                    _buyer.BuyerEmail = buyerm.BuyerEmail;
                    _buyer.BuyerWeb = buyerm.BuyerWeb;
                    _buyer.BuyerPANNo = buyerm.BuyerPANNo;
                    _buyer.BuyerCSTNo = buyerm.BuyerCSTNo;
                    _buyer.BuyerUPTTNo = buyerm.BuyerUPTTNo;
                    _buyer.BuyerTINNo = buyerm.BuyerTINNo;
                    _buyer.BuyerLocalTAX = buyerm.BuyerLocalTAX;

                    _buyer.BuyerTerms = buyerm.BuyerTerms;
                    _buyer.BuyerDesc = buyerm.BuyerDesc;
                    _buyer.PType = 1;
                    _buyer.BlackListed = 0;
                    _buyer.CurrencyID = buyerm.CID;
                    _buyer.BuyerUserID = buyerm.BuyerEmail;// buyerm.BuyerUserID;
                    _buyer.BuyerPW = buyerm.BuyerPW;
                    _buyer.FOB_CIF = buyerm.FOB_CIF;
                    _buyer.LogoPath = buyerm.LogoPath;
                    if (file != null)  //for buyer image
                    {
                        var webRoot = _env.WebRootPath;
                        string buyerimagefilename = file.FileName;
                        int pos = file.FileName.LastIndexOf(".");
                        buyerimagefilename = file.FileName.Substring(0, pos);
                        buyerimagefilename = file.FileName.Replace(buyerimagefilename, _buyer.BuyerId.ToString());
                        var uploads = Path.Combine("uploads/buyerimage/", buyerimagefilename);
                        var path = Path.Combine(_env.WebRootPath, uploads);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        _buyer.LogoPath = uploads;
                    }
                    else
                    {
                        _buyer.LogoPath = buyerm.LogoPath;
                    }

                    _buyer.IsActive = buyerm.IsActive;
                    _buyer.IsAllowLogin = buyerm.IsAllowLogin;
                    _buyer.IsGeneralBuyer = buyerm.IsGeneralBuyer;
                    _buyer.Online_Transfer = 0;
                    List<BuyerConsigneeDetails> icollection = new List<BuyerConsigneeDetails>();
                    BuyerConsigneeDetails _buyerconsignee;
                    long buyerconsigneeid = _ibuyer.GetNewBuyerConsigneeId().Result;
                    for (int i = 0; i < buyerm.BuyerConsigneeModel.Count; i++)
                    {
                        if (buyerm.BuyerConsigneeModel[i].Selected == true)
                        {
                            _buyerconsignee = new BuyerConsigneeDetails();
                            _buyerconsignee.BuyerConsigneeId = buyerconsigneeid;
                            _buyerconsignee.BuyerId = _buyer.BuyerId;
                            _buyerconsignee.ConsigneeId = buyerm.BuyerConsigneeModel[i].ConsigneeId;
                            _buyerconsignee.EntryDate = DateTime.Now.Date;
                            _buyerconsignee.SessionYear = Helper.Create_Session();
                            _buyerconsignee.UserId = HttpContext.Session.GetInt32("userid").Value;
                            buyerconsigneeid++;
                            icollection.Add(_buyerconsignee);
                        }
                    }

                    _buyer.BuyerConsigneeDetails = icollection;

                    string result = _ibuyer.Put(_buyer).Result;
                    ModelState.Clear();
                    //return Ok(result);
                    return RedirectToAction("BuyerList", "Buyer");
                }
                return View(buyerm);
            }
            catch (Exception ex)
            {
                return Json("EditBuyer " + ex.Message);
            }
        }

        [HttpGet]
        public ActionResult BuyerBank(long id)
        {
            try
            {
                BuyerDetails _buyerdetails = new BuyerDetails();
                _buyerdetails = _ibuyer.Get(Convert.ToInt32(id)).Result;
                List<ViewBuyerBank> _list = new List<ViewBuyerBank>();
                _list = _ibuyer.GetBuyerBank(id).Result;
                BuyerBankModel _buyerbankmodel;
                List<BuyerBankModel> _listmodel = new List<BuyerBankModel>();
                for (int i = 0; i < _list.Count; i++)
                {
                    _buyerbankmodel = new BuyerBankModel();
                    _buyerbankmodel.BuyerBankId = _list[i].BuyerBankId;
                    _buyerbankmodel.BuyerId = _list[i].BuyerId;
                    _buyerbankmodel.Beneficiary = _list[i].Beneficiary;
                    _buyerbankmodel.BankName = _list[i].BankName;
                    _buyerbankmodel.BankAddress = _list[i].BankAddress;
                    _buyerbankmodel.BankSwiftCode = _list[i].BankSwiftCode;
                    _buyerbankmodel.BankIFSCCode = _list[i].BankIFSCCode;
                    _buyerbankmodel.AccountNo = _list[i].AccountNo;
                    _listmodel.Add(_buyerbankmodel);
                }

                BuyerModel _buyermodel = new BuyerModel();
                _buyermodel.BuyerId = id;
                _buyermodel.BuyerCode = _buyerdetails.BuyerCode;
                _buyermodel.BuyerName = _buyerdetails.BuyerName;
                _buyermodel.BuyerAddress = _buyerdetails.BuyerAddress;
                _buyermodel.BuyerBankModel = _listmodel;
                _buyermodel.BankList = Helper.ConvertObjectToSelectList(_ibuyer.GetBankNameList().Result);
                return PartialView("BuyerBank", _buyermodel);
            }
            catch (Exception ex)
            {
                return Json("Buyer Bank " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult BuyerBank(BuyerModel _buyermodel)
        {
            try
            {
                BuyerBankDetails _buyerbank = new BuyerBankDetails();
                _buyerbank.BuyerBankId = _ibuyer.GetNewBuyerBankId().Result;
                _buyerbank.BuyerId = _buyermodel.BuyerId;
                _buyerbank.Beneficiary = _buyermodel.Beneficiary;
                _buyerbank.BankId = _buyermodel.BankId;
                _buyerbank.BankAddress = _buyermodel.BankAddress;
                _buyerbank.AccountNo = _buyermodel.AccountNo;
                _buyerbank.BankSwiftCode = _buyermodel.BankSwiftCode;
                _buyerbank.BankIFSCCode = _buyermodel.BankIFSCCode;
                _buyerbank.EntryDate = DateTime.Now.Date;
                _buyerbank.UserId = HttpContext.Session.GetInt32("userid").Value;
                _buyerbank.SessionYear = Helper.Create_Session();
                string result = _ibuyer.PostBuyerBank(_buyerbank).Result;
                return RedirectToAction("BuyerBank", "Buyer", new RouteValueDictionary(new { Id = _buyermodel.BuyerId }));
            }
            catch (Exception ex)
            {
                return Json("Buyer Bank " + ex.Message);
            }
        }

        public ActionResult DeleteBuyerBank(int buyerbankid, long buyerid)
        {
            try
            {
                _ibuyer.DeleteBuyerBank(buyerbankid);
                return RedirectToAction("BuyerBank", "Buyer", new RouteValueDictionary(new { Id = buyerid }));
            }
            catch (Exception ex)
            {
                return Json("DeleteBuyerFile " + ex.Message);
            }
        }
        
        public ActionResult BuyerPrint(long id)
        {
            try
            {
                IEnumerable<ViewBuyerDetails> data = _ibuyer.GetBuyerDetails(id).Result;
                DataTable table = new DataTable();
                table.TableName = "ViewBuyerDetails";
                using (var reader = ObjectReader.Create(data))
                {
                    table.Load(reader);
                }
                table.WriteXml(Path.Combine(_env.WebRootPath, "crystal/xml/ViewBuyerDetails.xml"), XmlWriteMode.WriteSchema);
                //Buyer Bank Sub Report
                IEnumerable<ViewBuyerBank> datachild = _ibuyer.GetBuyerBank(id).Result;
                DataTable tablechild = new DataTable();
                tablechild.TableName = "ViewBuyerBank";
                using (var reader = ObjectReader.Create(datachild))
                {
                    tablechild.Load(reader);
                }
                tablechild.WriteXml(Path.Combine(_env.WebRootPath, "crystal/xml/ViewBuyerBank.xml"), XmlWriteMode.WriteSchema);
                //Buyer Consignee
                IEnumerable<ViewBuyerConsignee> dataconsignee = _ibuyer.BuyerConsigneePrint(id).Result;
                DataTable tableconsignee = new DataTable();
                tableconsignee.TableName = "ViewBuyerConsignee";
                using (var reader = ObjectReader.Create(dataconsignee))
                {
                    tableconsignee.Load(reader);
                }
                tableconsignee.WriteXml(Path.Combine(_env.WebRootPath, "crystal/xml/ViewBuyerConsignee.xml"), XmlWriteMode.WriteSchema);
                ReportClass rptH = new ReportClass();
                rptH.FileName = Path.Combine(_env.WebRootPath, "crystal/rpt/BuyerProfile.rpt");
                rptH.Load();
                rptH.SetDataSource(table);
                rptH.Subreports[0].SetDataSource(tableconsignee);
                rptH.Subreports[1].SetDataSource(tablechild);
                Stream stream = rptH.ExportToStream(ExportFormatType.PortableDocFormat);
                return File(stream, "application/pdf");
            }
            catch (Exception ex)
            {
                return Json("BuyerPrint " + ex.Message);
            }
        }

        public JsonResult RefreshCountry()
        {
            try
            {
                return Json(_ibuyer.GetCountryList().Result);
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
                return Json(_ibuyer.GetCurrencyList().Result);
            }
            catch (Exception ex)
            {
                return Json("GetCurrencyList " + ex.Message);   
            }
        }

    }
}
