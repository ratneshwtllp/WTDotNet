using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using ERP.Web.Areas.RM.Models;
using ERP.Web.Areas.RMProperty.Models;
using FastMember;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Web.Areas.RM.Controllers
{
    [AuthLogin]
    [Area("RM")]
    public class RMController : Controller
    {
        RawMaterialModel RMM = new RawMaterialModel();
        IHostingEnvironment _env;
        IRMContract _rm;

        public RMController(IHostingEnvironment env, IRMContract rm)
        {
            _env = env;
            _rm = rm;
        }

        public ActionResult RMList()
        {
            try
            {
                RMM = new RawMaterialModel();
                RMM.RMCategoryList = Helper.ConvertObjectToSelectList(_rm.GetCategoryList().Result);
                RMM.RMSubCategoryList = Helper.FillDropDownList_Empty();
                RMM.ColorList = Helper.ConvertObjectToSelectList(_rm.GetColorDetails(string.Empty).Result);
                RMM.CreateDateFrom = DateTime.Now.Date;
                RMM.CreateDateTo = DateTime.Now.Date;
                RMM.RMBrandList = Helper.ConvertObjectToSelectList(_rm.GetListRMBrand(string.Empty).Result);
                RMM.RMUpdateOn = DateTime.Now.Date;
                RMM.RMReviewOn = DateTime.Now.Date;
                return View(RMM);
            }
            catch (Exception ex)
            {
                return Json("RMList " + ex.Message);
            }
        }

        public ActionResult RMList_Partial(RMSearch rms)
        {
            try
            {
                var data = _rm.RMList(rms).Result;
                return PartialView("_RMList", data);
            }
            catch (Exception ex)
            {
                return Json("RMList_Partial " + ex.Message);
            }
        }

        public ActionResult RMListForAll()
        {
            try
            {
                RMM = new RawMaterialModel();
                RMM.RMCategoryList = Helper.ConvertObjectToSelectList(_rm.GetCategoryList().Result);
                RMM.RMSubCategoryList = Helper.FillDropDownList_Empty();
                RMM.ColorList = Helper.ConvertObjectToSelectList(_rm.GetColorDetails(string.Empty).Result);
                RMM.CreateDateFrom = DateTime.Now.Date;
                RMM.CreateDateTo = DateTime.Now.Date;
                RMM.RMBrandList = Helper.ConvertObjectToSelectList(_rm.GetListRMBrand(string.Empty).Result);
                RMM.RMUpdateOn = DateTime.Now.Date;
                RMM.RMReviewOn = DateTime.Now.Date;
                return View(RMM);
            }
            catch (Exception ex)
            {
                return Json("RMListForAll " + ex.Message);
            }
        }

        public ActionResult RMListForAll_Partial(RMSearch rms)
        {
            try
            {
                var data = _rm.RMList(rms).Result;
                return PartialView("_RMListForAll", data);
            }
            catch (Exception ex)
            {
                return Json("RMListForAll_Partial " + ex.Message);
            }
        }

        public ActionResult CreateRM()
        {
            try
            {
                RMM = new RawMaterialModel();
                RMM.EditMode = false;
                RMM.RMCategoryList = Helper.ConvertObjectToSelectList(_rm.GetCategoryList().Result);
                RMM.RMSubCategoryList = Helper.FillDropDownList_Empty();
                RMM.Code = "";
                //RMM.Code = _rm.GetNewRMId().Result.ToString();
                //RMM.FinishList = Helper.ConvertObjectToSelectList_WithText(_rm.GetFinishMetalDetails().Result);
                //RMM.ThicknessList = Helper.ConvertObjectToSelectList(_rm.GetThicknessDetails().Result);
                RMM.ColorList = Helper.ConvertObjectToSelectList(_rm.GetColorDetails(string.Empty).Result);
                //RMM.ShapeList = Helper.ConvertObjectToSelectList(_rm.GetShapeDetails(string.Empty).Result);
                RMM.SizeList = Helper.ConvertObjectToSelectList(_rm.GetSizeDetails(string.Empty).Result);
                //RMM.MetalList = Helper.ConvertObjectToSelectList(_rm.GetMetalDetails(string.Empty).Result);
                //RMM.WiresizeList = RMM.SizeList;
                //RMM.StoneList = Helper.ConvertObjectToSelectList(_rm.GetStoneDetails(string.Empty).Result);
                //RMM.PinThicknessList = RMM.ThicknessList;
                RMM.PurchaseUnitList = Helper.ConvertObjectToSelectList(_rm.GetUnitDetails(string.Empty).Result);
                RMM.CostUnitList = RMM.PurchaseUnitList;
                //RMM.CartoonList = Helper.ConvertObjectToSelectList(_rm.GetCartoonDetails(string.Empty).Result);
                //RMM.TanningList = Helper.ConvertObjectToSelectList(_rm.GetTanningDetails(string.Empty).Result);
                //RMM.QualityList = Helper.ConvertObjectToSelectList(_rm.GetQualityDetails(string.Empty).Result);
                //RMM.StructureList = Helper.ConvertObjectToSelectList(_rm.GetStructureDetails(string.Empty).Result);
                //RMM.SelectionList = Helper.ConvertObjectToSelectList(_rm.GetSelectionDetails(string.Empty).Result);
                //RMM.AdhesiveList = Helper.ConvertObjectToSelectList(_rm.GetAdhesiveDetails(string.Empty).Result);
                //RMM.ClothList = Helper.ConvertObjectToSelectList(_rm.GetClothDetails(string.Empty).Result);
                ////RMM.SupplierList = Helper.ConvertObjectToSelectList(_rm.GetSupplierDetails().Result);
                //RMM.WidthList = Helper.ConvertObjectToSelectList(_rm.GetWidthDetails(string.Empty).Result);
                //RMM.GSMList = Helper.ConvertObjectToSelectList(_rm.GetGSMDetails().Result);
                //RMM.RMBrandList = Helper.ConvertObjectToSelectList(_rm.GetListRMBrand(string.Empty).Result);
                //RMM.RMBrandId = 1;
                RMM.HSNList = Helper.ConvertObjectToSelectList(_rm.GetHSNList().Result);
                RMM.CertificateRequiredForPuchaseList = Helper.FillDropDownList_YesNoWithValue();
                RMM.ConversionFactor = 1;

                RMM.Title = Path.Combine("uploads/rmphotos/rmphoto.jpg"); ;
                RMM.CreatedDate = DateTime.Now.Date;
                List<SupplierDetails> s = _rm.GetSupplierList().Result;
                List<RItemSuppModel> _RItemSuppModel = new List<RItemSuppModel>();
                RItemSuppModel _onesupplier;
                for (var i = 0; i < s.Count; i++)
                {
                    _onesupplier = new RItemSuppModel();
                    _onesupplier.SupplierId = s[i].SupplierId;
                    _onesupplier.SupplierName = s[i].SupplierName;
                    _onesupplier.Selected = false;
                    lock (_RItemSuppModel)
                    {
                        _RItemSuppModel.Add(_onesupplier);
                    }
                }
                List<QuickTestDetails> QuickTestList = _rm.GetQuickTestList().Result;
                List<QuickTestModel> _QuickTestModel = new List<QuickTestModel>();
                QuickTestModel _QuickTest;
                for (var i = 0; i < QuickTestList.Count; i++)
                {
                    _QuickTest = new QuickTestModel();
                    _QuickTest.ID = QuickTestList[i].ID;
                    _QuickTest.TestName = QuickTestList[i].TestName;
                    _QuickTest.Selected = false;
                    lock (_QuickTestModel)
                    {
                        _QuickTestModel.Add(_QuickTest);
                    }
                }
                RMM.RItemSuppModel = _RItemSuppModel;
                RMM.QuickTestModel = _QuickTestModel;
                return View(RMM);
            }
            catch (Exception ex)
            {
                return Json("CreateRM " + ex.Message);
            }
        }

        [HttpPost, ActionName("CreateRM")]
        public async Task<IActionResult> CreateRM(RawMaterialModel RMM, IFormFile file)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                RitemMaster _RawMaterial = new RitemMaster();
                if (RMM.RItem_ID == 0)
                    _RawMaterial.RitemId = _rm.GetNewRMId().Result;
                else
                    _RawMaterial.RitemId = RMM.RItem_ID;

                _RawMaterial.Title = RMM.Title;

                if (file != null)
                {
                    var webRoot = _env.WebRootPath;
                    //var uploads = Path.Combine(_env.WebRootPath, "uploads/rmphotos");
                    string rmfilename = file.FileName;
                    int pos = file.FileName.LastIndexOf(".");
                    rmfilename = file.FileName.Substring(0, pos);
                    rmfilename = file.FileName.Replace(rmfilename, _RawMaterial.RitemId.ToString());
                    var uploads = Path.Combine("uploads/rmphotos/", rmfilename);
                    var path = Path.Combine(_env.WebRootPath, uploads);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    _RawMaterial.Title = uploads;
                }

                _RawMaterial.Name = RMM.Name;
                _RawMaterial.Code = RMM.Code;
                _RawMaterial.BelongTo = RMM.Subcat_Id;
                _RawMaterial.MasterBelongTo = RMM.Cat_Id;
                _RawMaterial.ItOrCat = 1;
                _RawMaterial.Finish = "-";
                _RawMaterial.Colour = "-"; //RMM.Colour;
                _RawMaterial.Shape = "-";// RMM.Shape;
                _RawMaterial.NickleFree = "-";// RMM.NickleFree;

                _RawMaterial.SizeName = "-"; //RMM.SizeName;
                _RawMaterial.WireSize = "-"; //RMM.WireSize;
                _RawMaterial.Stone = "-";// RMM.Stone;
                _RawMaterial.Dim1 = "-"; //RMM.Dim1;
                _RawMaterial.Dim2 = "";
                _RawMaterial.Diameter = "";
                _RawMaterial.PickThickness = "-";// RMM.PickThickness;
                _RawMaterial.Punit = RMM.PUnit;
                _RawMaterial.PucrhasePrice = 0;
                _RawMaterial.CostUnit = RMM.CostUnit;
                _RawMaterial.CostPrice = 0;
                _RawMaterial.ConversionFactor = RMM.ConversionFactor;

                _RawMaterial.OpStock = 0;
                _RawMaterial.MinStock = RMM.Min_Stock;
                _RawMaterial.MaxStock = RMM.Max_Stock;
                _RawMaterial.Was = RMM.WAS;
                _RawMaterial.BomWasPercent = RMM.BOM_WAS_Percent;
                _RawMaterial.PanningWay = "-";// RMM.PanningWay; 
                _RawMaterial.Stucture = "-";// RMM.Stucture; 
                _RawMaterial.Washable = "-";// RMM.Washable; 
                _RawMaterial.AverageArea = "-"; //RMM.AverageArea;
                _RawMaterial.Selection = "-";// RMM.Selection;
                _RawMaterial.StrenthTest = "-"; //RMM.StrenthTest; 
                _RawMaterial.Base = "-";// RMM.Base; ;
                _RawMaterial.DyedSpe = "-"; // RMM.DyedSpe; ;
                _RawMaterial.EuNorms = "-"; // RMM.EuNorms; ;

                _RawMaterial.MinDeliveryDays = RMM.MinDeliveryDays;
                _RawMaterial.Dnsity = "-"; //RMM.Dnsity;
                _RawMaterial.SessionYear = Helper.Create_Session();
                _RawMaterial.UserId = HttpContext.Session.GetInt32("userid").Value;
                _RawMaterial.ColorFast = "-";/// RMM.ColorFast;
                _RawMaterial.Description = RMM.Description;
                _RawMaterial.Cloth = "-"; //RMM.Cloth; ;
                _RawMaterial.Metal = "-";// RMM.Metal;
                _RawMaterial.Quality = "-";// RMM.Quality; ;
                _RawMaterial.Adhesive = "-";// RMM.Adhesive;
                _RawMaterial.Cartoon = "-";// RMM.Cartoon;
                _RawMaterial.FullName = RMM.Name;

                _RawMaterial.PurchaseType = "";
                _RawMaterial.ActualPunit = "";  // RMM.PUnit;
                _RawMaterial.ActualPurPrice = RMM.ActualPurPrice;
                _RawMaterial.ActualCostUnit = "";  //RMM.CostUnit;
                _RawMaterial.ActualCostPrice = 0;
                _RawMaterial.ActualConversionFactor = 0;// RMM.ConversionFactor;
                _RawMaterial.RmCode = _rm.GetNewRMCodeNumeric(RMM.Cat_Id).Result;
                _RawMaterial.SuppCode = RMM.SuppCode;
                _RawMaterial.MonthYear = 0;
                _RawMaterial.MaxWastage = 0;// RMM.Max_Wastage;

                _RawMaterial.Thickness = "-";// RMM.Thickness; // "";
                _RawMaterial.Design = "";
                _RawMaterial.Width = "-";// RMM.Width;
                _RawMaterial.GSM = 0;// RMM.GSM;
                _RawMaterial.RMBrandId = 1;//RMM.RMBrandId;
                _RawMaterial.CreatedDate = DateTime.Now.Date;
                _RawMaterial.HSNId = RMM.HSNId;
                _RawMaterial.IsCertificateRequiredForPurchase = RMM.CertReqForPuchaseId;

                _RawMaterial.RMUpdateOn = DateTime.Now.Date;
                _RawMaterial.RMUpdateUser = HttpContext.Session.GetInt32("userid").Value;  // 0;
                _RawMaterial.RMReviewStatus = 0;
                _RawMaterial.RMReviewOn = DateTime.Now.Date;
                _RawMaterial.RMReviewUser = 0; // HttpContext.Session.GetInt32("userid").Value;
                _RawMaterial.CLID = RMM.ColorId;
                _RawMaterial.SizeID = RMM.SizeID;

                _RawMaterial.ActualPunit = "";  // RMM.PUnit;
                if (RMM.RItemSuppModel != null)
                {
                    List<RItemSupp> icollection = new List<RItemSupp>();
                    RItemSupp _RItemSupp;
                    long ritemsuppid = _rm.GetNewRMSuppId().Result;
                    for (int i = 0; i < RMM.RItemSuppModel.Count; i++)
                    {
                        if (RMM.RItemSuppModel[i].Selected == true)
                        {
                            _RItemSupp = new RItemSupp();
                            _RItemSupp.RItem_Id = _RawMaterial.RitemId;
                            _RItemSupp.SupplierId = RMM.RItemSuppModel[i].SupplierId;
                            _RItemSupp.RItemSuppID = ritemsuppid;
                            _RItemSupp.Price = 0;
                            _RItemSupp.MinDays = 0;
                            _RItemSupp.SuppUnit = RMM.PUnit; //unitid;
                            _RItemSupp.SupplierRMCode = "-";

                            ritemsuppid++;
                            lock (icollection)
                            {
                                icollection.Add(_RItemSupp);
                            }
                        }
                    }
                    _RawMaterial.RItemSupp = icollection;
                }
                

                if (RMM.RMPropertiesModel != null)
                {
                    List<RMChild> irmchild = new List<RMChild>();
                    RMChild _rmchild;
                    long rmchildid = _rm.GetNewRMChildId().Result;
                    for (int i = 0; i < RMM.RMPropertiesModel.Count; i++)
                    {
                        _rmchild = new RMChild();
                        _rmchild.RMChildId = rmchildid;
                        _rmchild.RItem_Id = _RawMaterial.RitemId;
                        _rmchild.RMPropertiesID = RMM.RMPropertiesModel[i].RMPropertiesID;
                        _rmchild.RMPropertiesValueID = RMM.RMPropertiesModel[i].RMPropertiesValueID;
                        if (RMM.RMPropertiesModel[i].RMPropertiesValueID >= 0)
                            _rmchild.RMPValue = "-";
                        else
                            _rmchild.RMPValue = RMM.RMPropertiesModel[i].RMPValue;
                        rmchildid++;
                        lock (irmchild)
                        {
                            irmchild.Add(_rmchild);
                        }

                    }

                    _RawMaterial.RMChild = irmchild;
                }
                List<RItemQuickTest> QuickTestcollection = new List<RItemQuickTest>();

                RItemQuickTest _QuickTest;
                long QuickTestId = _rm.GetNewRItemQuickTestId().Result;
                for (int i = 0; i < RMM.QuickTestModel.Count; i++)
                {
                    if (RMM.QuickTestModel[i].Selected == true)
                    {
                        _QuickTest = new RItemQuickTest();
                        _QuickTest.RItem_Id = _RawMaterial.RitemId;
                        _QuickTest.QuickTestId = RMM.QuickTestModel[i].ID;
                        _QuickTest.RItemQuickTestId = QuickTestId;
                        QuickTestId++;

                        lock (QuickTestcollection)
                        {
                            QuickTestcollection.Add(_QuickTest);
                        }
                    }
                }
                _RawMaterial.RItemQuickTest = QuickTestcollection;
                string result = _rm.Post(_RawMaterial).Result;
                ModelState.Clear();
                //return RedirectToAction("RMList");

                return RedirectToAction("CreateRM");
            }
            catch (Exception ex)
            {
                return Json("CreateRM " + ex.Message);
            }
        }

        public ActionResult EditRM(int id)
        {
            try
            {
                RMM = new RawMaterialModel();
                RitemMaster _RawMaterial = new RitemMaster();
                _RawMaterial = _rm.Get(id).Result;
                RMM.EditMode = true;
                RMM.RItem_ID = id;
                RMM.Cat_Id = _RawMaterial.MasterBelongTo;
                RMM.Subcat_Id = _RawMaterial.BelongTo;
                RMM.Code = _RawMaterial.Code;
                RMM.Name = _RawMaterial.Name;
                RMM.PUnit = _RawMaterial.Punit;
                RMM.CostUnit = _RawMaterial.CostUnit;
                RMM.ConversionFactor = _RawMaterial.ConversionFactor;
                RMM.Min_Stock = _RawMaterial.MinStock;
                RMM.Max_Stock = _RawMaterial.MaxStock;
                RMM.WAS = _RawMaterial.Was;
                RMM.BOM_WAS_Percent = _RawMaterial.BomWasPercent;
                RMM.MinDeliveryDays = _RawMaterial.MinDeliveryDays;
                //RMM.Max_Wastage = _RawMaterial.MaxWastage;
                RMM.Description = _RawMaterial.Description;
                RMM.SuppCode = _RawMaterial.SuppCode;
                RMM.HSNId = _RawMaterial.HSNId;
                RMM.CertReqForPuchaseId = _RawMaterial.IsCertificateRequiredForPurchase;
                RMM.CertificateRequiredForPuchaseList = Helper.FillDropDownList_YesNoWithValue();

                RMM.HSNList = Helper.ConvertObjectToSelectList(_rm.GetHSNList().Result);
                RMM.RMCategoryList = Helper.ConvertObjectToSelectList(_rm.GetCategoryList().Result);
                RMM.RMSubCategoryList = Helper.ConvertObjectToSelectList(_rm.GetSubCategoryList(RMM.Cat_Id).Result);
                RMM.ColorList = Helper.ConvertObjectToSelectList(_rm.GetColorDetails(string.Empty).Result);
                RMM.SizeList = Helper.ConvertObjectToSelectList(_rm.GetSizeDetails(string.Empty).Result);
                RMM.ColorId = _RawMaterial.CLID;
                RMM.SizeID = _RawMaterial.SizeID;
                RMM.ActualPurPrice = _RawMaterial.ActualPurPrice;

                RMM.PurchaseUnitList = Helper.ConvertObjectToSelectList(_rm.GetUnitDetails(string.Empty).Result);
                RMM.CostUnitList = RMM.PurchaseUnitList;
                List<RItemSupp> existingSupp = _rm.GetRMSupplier(id).Result;
                List<SupplierDetails> supplierList = _rm.GetSupplierList().Result;
                List<RItemSuppModel> _RItemSuppModel = new List<RItemSuppModel>();
                RItemSuppModel _onesupplier;
                bool supplier_found = false;
                for (var i = 0; i < supplierList.Count; i++)
                {
                    supplier_found = false;
                    _onesupplier = new RItemSuppModel();
                    _onesupplier.SupplierId = supplierList[i].SupplierId;
                    _onesupplier.SupplierName = supplierList[i].SupplierName;
                    _onesupplier.Selected = false;

                    for (var j = 0; j < existingSupp.Count; j++)
                    {
                        if (existingSupp[j].SupplierId == supplierList[i].SupplierId)
                        {
                            supplier_found = true;
                            break;
                        }
                    }

                    if (supplier_found)
                        _onesupplier.Selected = true;

                    lock (_RItemSuppModel)
                    {
                        _RItemSuppModel.Add(_onesupplier);
                    }
                }
                List<RItemQuickTest> existingQuickTest = _rm.GetExQuickTestList(Convert.ToInt64(id)).Result;
                List<QuickTestDetails> QuickTestList = _rm.GetQuickTestList().Result;

                List<QuickTestModel> QuickTestModel = new List<QuickTestModel>();
                QuickTestModel _QuickTest;
                bool QuickTest_found = false;
                for (var i = 0; i < QuickTestList.Count; i++)
                {
                    QuickTest_found = false;
                    _QuickTest = new QuickTestModel();
                    _QuickTest.ID = QuickTestList[i].ID;
                    _QuickTest.TestName = QuickTestList[i].TestName;
                    _QuickTest.Selected = false;

                    for (var j = 0; j < existingQuickTest.Count; j++)
                    {
                        if (existingQuickTest[j].QuickTestId == QuickTestList[i].ID)
                        {
                            QuickTest_found = true;
                            break;
                        }
                    }

                    if (QuickTest_found)
                        _QuickTest.Selected = true;

                    lock (QuickTestModel)
                    {
                        QuickTestModel.Add(_QuickTest);
                    }
                }
                _RItemSuppModel = _RItemSuppModel.OrderByDescending(x => x.Selected).ToList();
                QuickTestModel = QuickTestModel.OrderByDescending(x => x.Selected).ToList();

                RMM.RItemSuppModel = _RItemSuppModel;
                RMM.QuickTestModel = QuickTestModel;

                RMM.Title = _RawMaterial.Title;
                return View("CreateRM", RMM);
            }
            catch (Exception ex)
            {
                return Json("EditRM " + ex.Message);
            }
        }

        [HttpPost, ActionName("EditRM")]
        public async Task<IActionResult> EditRM(RawMaterialModel RMM, IFormFile file)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                RitemMaster _RawMaterial = new RitemMaster();

                if (RMM.RItem_ID == 0)
                    _RawMaterial.RitemId = _rm.GetNewRMId().Result;
                else
                    _RawMaterial.RitemId = RMM.RItem_ID;

                _RawMaterial.Title = RMM.Title;

                if (file != null)
                {
                    var webRoot = _env.WebRootPath;
                    //var uploads = Path.Combine(_env.WebRootPath, "uploads/rmphotos");
                    string rmfilename = file.FileName;
                    int pos = file.FileName.LastIndexOf(".");
                    rmfilename = file.FileName.Substring(0, pos);
                    rmfilename = file.FileName.Replace(rmfilename, _RawMaterial.RitemId.ToString());
                    var uploads = Path.Combine("uploads/rmphotos/", rmfilename);
                    var path = Path.Combine(_env.WebRootPath, uploads);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    _RawMaterial.Title = uploads;
                }

                _RawMaterial.BelongTo = RMM.Subcat_Id;
                _RawMaterial.MasterBelongTo = RMM.Cat_Id;
                _RawMaterial.Name = RMM.Name;
                _RawMaterial.Code = RMM.Code;
                _RawMaterial.CLID = RMM.ColorId;
                _RawMaterial.SizeID = RMM.SizeID;
                _RawMaterial.HSNId = RMM.HSNId;
                _RawMaterial.Punit = RMM.PUnit;
                _RawMaterial.CostUnit = RMM.CostUnit;
                _RawMaterial.ConversionFactor = RMM.ConversionFactor;
                _RawMaterial.MinStock = RMM.Min_Stock;
                _RawMaterial.MaxStock = RMM.Max_Stock;
                _RawMaterial.Was = RMM.WAS;
                _RawMaterial.BomWasPercent = RMM.BOM_WAS_Percent;
                _RawMaterial.Description = RMM.Description;
                _RawMaterial.FullName = RMM.Name;
                _RawMaterial.IsCertificateRequiredForPurchase = RMM.CertReqForPuchaseId;
                _RawMaterial.RMUpdateOn = DateTime.Now.Date;
                _RawMaterial.RMUpdateUser = HttpContext.Session.GetInt32("userid").Value;
                _RawMaterial.RMReviewStatus = 0;
                _RawMaterial.RMReviewOn = DateTime.Now.Date;
                _RawMaterial.RMReviewUser = 0; // HttpContext.Session.GetInt32("userid").Value;
                _RawMaterial.MaxWastage = RMM.Max_Wastage;
                _RawMaterial.SuppCode = RMM.SuppCode;

                _RawMaterial.ActualPurPrice = RMM.ActualPurPrice;
                List<RItemSupp> icollection = new List<RItemSupp>();
                if (RMM.RItemSuppModel != null)
                {
                    RItemSupp _RItemSupp;
                    long ritemsuppid = _rm.GetNewRMSuppId().Result;
                    for (int i = 0; i < RMM.RItemSuppModel.Count; i++)
                    {
                        if (RMM.RItemSuppModel[i].Selected == true)
                        {
                            _RItemSupp = new RItemSupp();
                            _RItemSupp.RItem_Id = _RawMaterial.RitemId;
                            _RItemSupp.SupplierId = RMM.RItemSuppModel[i].SupplierId;
                            _RItemSupp.RItemSuppID = ritemsuppid;
                            _RItemSupp.Price = 0;
                            _RItemSupp.MinDays = 0;
                            _RItemSupp.SuppUnit = RMM.PUnit; //unitid;
                            _RItemSupp.SupplierRMCode = "-";
                            ritemsuppid++;
                            lock (icollection)
                            {
                                icollection.Add(_RItemSupp);
                            }
                        }
                    }

                    List<RItemQuickTest> QuickTestcollection = new List<RItemQuickTest>();

                    RItemQuickTest _QuickTest;
                    long QuickTestId = _rm.GetNewRItemQuickTestId().Result;
                    for (int i = 0; i < RMM.QuickTestModel.Count; i++)
                    {
                        if (RMM.QuickTestModel[i].Selected == true)
                        {
                            _QuickTest = new RItemQuickTest();
                            _QuickTest.RItem_Id = _RawMaterial.RitemId;
                            _QuickTest.QuickTestId = RMM.QuickTestModel[i].ID;
                            _QuickTest.RItemQuickTestId = QuickTestId;
                            QuickTestId++;

                            lock (QuickTestcollection)
                            {
                                QuickTestcollection.Add(_QuickTest);
                            }
                        }
                    }

                    _RawMaterial.RItemSupp = icollection;
                    _RawMaterial.RItemQuickTest = QuickTestcollection;
                }
                if (RMM.RMPropertiesModel != null)
                {
                    List<RMChild> irmchild = new List<RMChild>();
                    RMChild _rmchild;
                    long rmchildid = _rm.GetNewRMChildId().Result;
                    for (int i = 0; i < RMM.RMPropertiesModel.Count; i++)
                    {
                        _rmchild = new RMChild();
                        _rmchild.RMChildId = rmchildid;
                        _rmchild.RItem_Id = _RawMaterial.RitemId;
                        _rmchild.RMPropertiesID = RMM.RMPropertiesModel[i].RMPropertiesID;
                        _rmchild.RMPropertiesValueID = RMM.RMPropertiesModel[i].RMPropertiesValueID;
                        //if (RMM.RMPropertiesModel[i].RMPropertiesIsMaster  == 0)
                        //    _rmchild.RMPValue = "-";
                        //else
                        _rmchild.RMPValue = RMM.RMPropertiesModel[i].RMPValue;
                        rmchildid++;
                        lock (irmchild)
                        {
                            irmchild.Add(_rmchild);
                        }

                    }

                    _RawMaterial.RMChild = irmchild;
                }
                string result = _rm.PutRM(_RawMaterial).Result;
                ModelState.Clear();
                return RedirectToAction("RMList");
                //}
                //return RedirectToAction("CreateRM");
            }
            catch (Exception ex)
            {
                return Json("EditRM " + ex.Message);
            }
        }

        //public ActionResult CreateRM()
        //{
        //    try
        //    {
        //        RMM = new RawMaterialModel();
        //        RMM.RMCategoryList = Helper.ConvertObjectToSelectList(_rm.GetCategoryList().Result);
        //        RMM.RMSubCategoryList = Helper.FillDropDownList_Empty();
        //        RMM.FinishList = Helper.ConvertObjectToSelectList_WithText(_rm.GetFinishMetalDetails().Result);
        //        RMM.ThicknessList = Helper.ConvertObjectToSelectList(_rm.GetThicknessDetails().Result);
        //        RMM.ColorList = Helper.ConvertObjectToSelectList(_rm.GetColorDetails(string.Empty).Result);
        //        RMM.ShapeList = Helper.ConvertObjectToSelectList(_rm.GetShapeDetails(string.Empty).Result);
        //        RMM.SizeList = Helper.ConvertObjectToSelectList(_rm.GetSizeDetails(string.Empty).Result);
        //        RMM.MetalList = Helper.ConvertObjectToSelectList(_rm.GetMetalDetails(string.Empty).Result);
        //        RMM.WiresizeList = RMM.SizeList;
        //        RMM.StoneList = Helper.ConvertObjectToSelectList(_rm.GetStoneDetails(string.Empty).Result);
        //        RMM.PinThicknessList = RMM.ThicknessList;
        //        RMM.PurchaseUnitList = Helper.ConvertObjectToSelectList(_rm.GetUnitDetails(string.Empty).Result);
        //        RMM.CostUnitList = RMM.PurchaseUnitList;
        //        RMM.CartoonList = Helper.ConvertObjectToSelectList(_rm.GetCartoonDetails(string.Empty).Result);
        //        RMM.TanningList = Helper.ConvertObjectToSelectList(_rm.GetTanningDetails(string.Empty).Result);
        //        RMM.QualityList = Helper.ConvertObjectToSelectList(_rm.GetQualityDetails(string.Empty).Result);
        //        RMM.StructureList = Helper.ConvertObjectToSelectList(_rm.GetStructureDetails(string.Empty).Result);
        //        RMM.SelectionList = Helper.ConvertObjectToSelectList(_rm.GetSelectionDetails(string.Empty).Result);
        //        RMM.AdhesiveList = Helper.ConvertObjectToSelectList(_rm.GetAdhesiveDetails(string.Empty).Result);
        //        RMM.ClothList = Helper.ConvertObjectToSelectList(_rm.GetClothDetails(string.Empty).Result);
        //        //RMM.SupplierList = Helper.ConvertObjectToSelectList(_rm.GetSupplierDetails().Result);
        //        RMM.WidthList = Helper.ConvertObjectToSelectList(_rm.GetWidthDetails(string.Empty).Result);
        //        RMM.GSMList = Helper.ConvertObjectToSelectList(_rm.GetGSMDetails().Result);
        //        RMM.RMBrandList = Helper.ConvertObjectToSelectList(_rm.GetListRMBrand(string.Empty).Result);
        //        RMM.RMBrandId = 1;
        //        RMM.HSNList = Helper.ConvertObjectToSelectList(_rm.GetHSNList().Result);
        //        RMM.CertificateRequiredForPuchaseList = Helper.FillDropDownList_YesNoWithValue();
        //        RMM.ConversionFactor = 1;

        //        RMM.Title = Path.Combine("uploads/rmphotos/rmphoto.jpg"); ;
        //        RMM.CreatedDate = DateTime.Now.Date;
        //        List<SupplierDetails> s = _rm.GetSupplierList().Result;
        //        List<RItemSuppModel> _RItemSuppModel = new List<RItemSuppModel>();
        //        RItemSuppModel _onesupplier;
        //        for (var i = 0; i < s.Count; i++)
        //        {
        //            _onesupplier = new RItemSuppModel();
        //            _onesupplier.SupplierId = s[i].SupplierId;
        //            _onesupplier.SupplierName = s[i].SupplierName;
        //            _onesupplier.Selected = false;
        //            lock (_RItemSuppModel)
        //            {
        //                _RItemSuppModel.Add(_onesupplier);
        //            }
        //        }
        //        RMM.RItemSuppModel = _RItemSuppModel;
        //        return View(RMM);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json("CreateRM " + ex.Message);
        //    }
        //}

        //[HttpPost, ActionName("CreateRM")]
        //public async Task<IActionResult> CreateRM(RawMaterialModel RMM, IFormFile file)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            RitemMaster _RawMaterial = new RitemMaster();

        //            if (RMM.RItem_ID == 0)
        //                _RawMaterial.RitemId = _rm.GetNewRMId().Result;
        //            else
        //                _RawMaterial.RitemId = RMM.RItem_ID;

        //            _RawMaterial.Title = RMM.Title;

        //            if (file != null)
        //            {
        //                var webRoot = _env.WebRootPath;
        //                //var uploads = Path.Combine(_env.WebRootPath, "uploads/rmphotos");
        //                string rmfilename = file.FileName;
        //                int pos = file.FileName.LastIndexOf(".");
        //                rmfilename = file.FileName.Substring(0, pos);
        //                rmfilename = file.FileName.Replace(rmfilename, _RawMaterial.RitemId.ToString());
        //                var uploads = Path.Combine("uploads/rmphotos/", rmfilename);
        //                var path = Path.Combine(_env.WebRootPath, uploads);
        //                using (var fileStream = new FileStream(path, FileMode.Create))
        //                {
        //                    await file.CopyToAsync(fileStream);
        //                }
        //                _RawMaterial.Title = uploads;
        //            }

        //            _RawMaterial.Name = RMM.Name;
        //            _RawMaterial.Code = RMM.Code;
        //            _RawMaterial.BelongTo = RMM.Subcat_Id;
        //            _RawMaterial.MasterBelongTo = RMM.Cat_Id;
        //            _RawMaterial.ItOrCat = 1;
        //            _RawMaterial.Finish = RMM.Finish;
        //            _RawMaterial.Colour = RMM.Colour;
        //            _RawMaterial.Shape = RMM.Shape;
        //            _RawMaterial.NickleFree = RMM.NickleFree;

        //            _RawMaterial.SizeName = RMM.SizeName;
        //            _RawMaterial.WireSize = RMM.WireSize;
        //            _RawMaterial.Stone = RMM.Stone;
        //            _RawMaterial.Dim1 = RMM.Dim1;
        //            _RawMaterial.Dim2 = "";
        //            _RawMaterial.Diameter = "";
        //            _RawMaterial.PickThickness = RMM.PickThickness;
        //            _RawMaterial.Punit = RMM.PUnit;
        //            _RawMaterial.PucrhasePrice = 0;
        //            _RawMaterial.CostUnit = RMM.CostUnit;
        //            _RawMaterial.CostPrice = 0;
        //            _RawMaterial.ConversionFactor = RMM.ConversionFactor;

        //            _RawMaterial.OpStock = 0;
        //            _RawMaterial.MinStock = RMM.Min_Stock;
        //            _RawMaterial.MaxStock = RMM.Max_Stock;
        //            _RawMaterial.Was = RMM.WAS;
        //            _RawMaterial.BomWasPercent = RMM.BOM_WAS_Percent;
        //            _RawMaterial.PanningWay = RMM.PanningWay; ;
        //            _RawMaterial.Stucture = RMM.Stucture; ;
        //            _RawMaterial.Washable = RMM.Washable; ;
        //            _RawMaterial.AverageArea = RMM.AverageArea; ;
        //            _RawMaterial.Selection = RMM.Selection;
        //            _RawMaterial.StrenthTest = RMM.StrenthTest; ;
        //            _RawMaterial.Base = RMM.Base; ;
        //            _RawMaterial.DyedSpe = RMM.DyedSpe; ;
        //            _RawMaterial.EuNorms = RMM.EuNorms; ;

        //            _RawMaterial.MinDeliveryDays = RMM.MinDeliveryDays; ;
        //            _RawMaterial.Dnsity = RMM.Dnsity;
        //            _RawMaterial.SessionYear = Helper.Create_Session();
        //            _RawMaterial.UserId = HttpContext.Session.GetInt32("userid").Value;
        //            _RawMaterial.ColorFast = RMM.ColorFast;
        //            _RawMaterial.Description = RMM.Description;
        //            _RawMaterial.Cloth = RMM.Cloth; ;
        //            _RawMaterial.Metal = RMM.Metal;
        //            _RawMaterial.Quality = RMM.Quality; ;
        //            _RawMaterial.Adhesive = RMM.Adhesive;
        //            _RawMaterial.Cartoon = RMM.Cartoon;
        //            _RawMaterial.FullName = RMM.Name;

        //            _RawMaterial.PurchaseType = "";
        //            _RawMaterial.ActualPunit = "";  // RMM.PUnit;
        //            _RawMaterial.ActualPurPrice = 0;
        //            _RawMaterial.ActualCostUnit = "";  //RMM.CostUnit;
        //            _RawMaterial.ActualCostPrice = 0;
        //            _RawMaterial.ActualConversionFactor = RMM.ConversionFactor;
        //            _RawMaterial.RmCode = _rm.GetNewRMCodeNumeric(RMM.Cat_Id).Result;
        //            _RawMaterial.SuppCode = RMM.SuppCode;
        //            _RawMaterial.MonthYear = 0;
        //            _RawMaterial.MaxWastage = RMM.Max_Wastage;

        //            _RawMaterial.Thickness = RMM.Thickness; // "";
        //            _RawMaterial.Design = "";
        //            _RawMaterial.Width = RMM.Width;
        //            _RawMaterial.GSM = RMM.GSM;
        //            _RawMaterial.RMBrandId = RMM.RMBrandId;
        //            _RawMaterial.CreatedDate = DateTime.Now.Date;
        //            _RawMaterial.HSNId = RMM.HSNId;
        //            _RawMaterial.IsCertificateRequiredForPurchase = RMM.CertReqForPuchaseId;

        //            _RawMaterial.RMUpdateOn = DateTime.Now.Date;
        //            _RawMaterial.RMUpdateUser = HttpContext.Session.GetInt32("userid").Value;  // 0;
        //            _RawMaterial.RMReviewStatus = 0;
        //            _RawMaterial.RMReviewOn = DateTime.Now.Date;
        //            _RawMaterial.RMReviewUser = 0; // HttpContext.Session.GetInt32("userid").Value;

        //            List<RItemSupp> icollection = new List<RItemSupp>();

        //            RItemSupp _RItemSupp;
        //            long ritemsuppid = _rm.GetNewRMSuppId().Result;
        //            for (int i = 0; i < RMM.RItemSuppModel.Count; i++)
        //            {
        //                if (RMM.RItemSuppModel[i].Selected == true)
        //                {
        //                    _RItemSupp = new RItemSupp();
        //                    _RItemSupp.RItem_Id = _RawMaterial.RitemId;
        //                    _RItemSupp.SupplierId = RMM.RItemSuppModel[i].SupplierId;
        //                    _RItemSupp.RItemSuppID = ritemsuppid;
        //                    _RItemSupp.Price = 0;
        //                    _RItemSupp.MinDays = 0;
        //                    _RItemSupp.SuppUnit = RMM.PUnit; //unitid;
        //                    _RItemSupp.SupplierRMCode = "-";

        //                    ritemsuppid++;
        //                    lock (icollection)
        //                    {
        //                        icollection.Add(_RItemSupp);
        //                    }
        //                }
        //            }

        //            _RawMaterial.RItemSupp = icollection;

        //            string result = _rm.Post(_RawMaterial).Result;
        //            ModelState.Clear();
        //            return RedirectToAction("RMList");
        //        }
        //        return RedirectToAction("CreateRM");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json("CreateRM " + ex.Message);
        //    }
        //}

        //public ActionResult EditRM(int id)
        //{
        //    try
        //    {
        //        RMM = new RawMaterialModel();
        //        RitemMaster _RawMaterial = new RitemMaster();
        //        _RawMaterial = _rm.Get(id).Result;

        //        RMM.RItem_ID = id;
        //        RMM.Cat_Id = _RawMaterial.MasterBelongTo;
        //        RMM.Subcat_Id = _RawMaterial.BelongTo;

        //        RMM.Code = _RawMaterial.Code;
        //        RMM.Name = _RawMaterial.Name;
        //        RMM.Finish = _RawMaterial.Finish;
        //        RMM.Thickness = _RawMaterial.Thickness;
        //        RMM.Colour = _RawMaterial.Colour;
        //        RMM.Shape = _RawMaterial.Shape;
        //        RMM.NickleFree = _RawMaterial.NickleFree;
        //        RMM.SizeName = _RawMaterial.SizeName;
        //        RMM.Metal = _RawMaterial.Metal;
        //        RMM.Cartoon = _RawMaterial.Cartoon;
        //        RMM.PanningWay = _RawMaterial.PanningWay;
        //        RMM.Quality = _RawMaterial.Quality;
        //        RMM.Stucture = _RawMaterial.Stucture;
        //        RMM.Selection = _RawMaterial.Selection;
        //        RMM.Adhesive = _RawMaterial.Adhesive;
        //        RMM.Cloth = _RawMaterial.Cloth;
        //        RMM.AverageArea = _RawMaterial.AverageArea;
        //        RMM.StrenthTest = _RawMaterial.StrenthTest;
        //        RMM.Base = _RawMaterial.Base;
        //        RMM.DyedSpe = _RawMaterial.DyedSpe;
        //        RMM.EuNorms = _RawMaterial.EuNorms;
        //        RMM.Dnsity = _RawMaterial.Dnsity;
        //        RMM.ColorFast = _RawMaterial.ColorFast;
        //        RMM.Purchase_Type = _RawMaterial.PurchaseType;
        //        RMM.PUnit = _RawMaterial.Punit;
        //        RMM.CostUnit = _RawMaterial.CostUnit;
        //        RMM.ConversionFactor = _RawMaterial.ConversionFactor;
        //        RMM.Min_Stock = _RawMaterial.MinStock;
        //        RMM.Max_Stock = _RawMaterial.MaxStock;
        //        RMM.WAS = _RawMaterial.Was;
        //        RMM.BOM_WAS_Percent = _RawMaterial.BomWasPercent;
        //        RMM.MinDeliveryDays = _RawMaterial.MinDeliveryDays;
        //        RMM.Max_Wastage = _RawMaterial.MaxWastage;
        //        RMM.Description = _RawMaterial.Description;
        //        RMM.Width = _RawMaterial.Width;
        //        RMM.GSM = _RawMaterial.GSM;
        //        RMM.RMBrandId = _RawMaterial.RMBrandId;
        //        RMM.SuppCode = _RawMaterial.SuppCode;
        //        RMM.CreatedDate = _RawMaterial.CreatedDate;
        //        RMM.HSNId = _RawMaterial.HSNId;
        //        RMM.CertReqForPuchaseId = _RawMaterial.IsCertificateRequiredForPurchase;
        //        RMM.CertificateRequiredForPuchaseList = Helper.FillDropDownList_YesNoWithValue();

        //        RMM.HSNList = Helper.ConvertObjectToSelectList(_rm.GetHSNList().Result);
        //        RMM.RMCategoryList = Helper.ConvertObjectToSelectList(_rm.GetCategoryList().Result);
        //        RMM.RMSubCategoryList = Helper.ConvertObjectToSelectList(_rm.GetSubCategoryList(RMM.Cat_Id).Result);
        //        RMM.FinishList = Helper.ConvertObjectToSelectList_WithText(_rm.GetFinishMetalDetails().Result);
        //        RMM.ThicknessList = Helper.ConvertObjectToSelectList(_rm.GetThicknessDetails().Result);
        //        RMM.ColorList = Helper.ConvertObjectToSelectList(_rm.GetColorDetails(string.Empty).Result);
        //        RMM.ShapeList = Helper.ConvertObjectToSelectList(_rm.GetShapeDetails(string.Empty).Result);
        //        RMM.SizeList = Helper.ConvertObjectToSelectList(_rm.GetSizeDetails(string.Empty).Result);
        //        RMM.MetalList = Helper.ConvertObjectToSelectList(_rm.GetMetalDetails(string.Empty).Result);
        //        RMM.WiresizeList = RMM.SizeList;
        //        RMM.StoneList = Helper.ConvertObjectToSelectList(_rm.GetStoneDetails(string.Empty).Result);
        //        RMM.PinThicknessList = RMM.ThicknessList;
        //        RMM.PurchaseUnitList = Helper.ConvertObjectToSelectList(_rm.GetUnitDetails(string.Empty).Result);
        //        RMM.CostUnitList = RMM.PurchaseUnitList;
        //        RMM.CartoonList = Helper.ConvertObjectToSelectList(_rm.GetCartoonDetails(string.Empty).Result);
        //        RMM.TanningList = Helper.ConvertObjectToSelectList(_rm.GetTanningDetails(string.Empty).Result);
        //        RMM.QualityList = Helper.ConvertObjectToSelectList(_rm.GetQualityDetails(string.Empty).Result);
        //        RMM.StructureList = Helper.ConvertObjectToSelectList(_rm.GetStructureDetails(string.Empty).Result);
        //        RMM.SelectionList = Helper.ConvertObjectToSelectList(_rm.GetSelectionDetails(string.Empty).Result);
        //        RMM.AdhesiveList = Helper.ConvertObjectToSelectList(_rm.GetAdhesiveDetails(string.Empty).Result);
        //        RMM.ClothList = Helper.ConvertObjectToSelectList(_rm.GetClothDetails(string.Empty).Result);
        //        RMM.WidthList = Helper.ConvertObjectToSelectList(_rm.GetWidthDetails(string.Empty).Result);
        //        RMM.GSMList = Helper.ConvertObjectToSelectList(_rm.GetGSMDetails().Result); RMM.RMBrandList = Helper.ConvertObjectToSelectList(_rm.GetListRMBrand(string.Empty).Result);

        //        List<RItemSupp> existingSupp = _rm.GetRMSupplier(id).Result;
        //        List<SupplierDetails> supplierList = _rm.GetSupplierList().Result;

        //        List<RItemSuppModel> _RItemSuppModel = new List<RItemSuppModel>();
        //        RItemSuppModel _onesupplier;
        //        bool supplier_found = false;
        //        for (var i = 0; i < supplierList.Count; i++)
        //        {
        //            supplier_found = false;
        //            _onesupplier = new RItemSuppModel();
        //            _onesupplier.SupplierId = supplierList[i].SupplierId;
        //            _onesupplier.SupplierName = supplierList[i].SupplierName;
        //            _onesupplier.Selected = false;

        //            for (var j = 0; j < existingSupp.Count; j++)
        //            {
        //                if (existingSupp[j].SupplierId == supplierList[i].SupplierId)
        //                {
        //                    supplier_found = true;
        //                    break;
        //                }
        //            }

        //            if (supplier_found)
        //                _onesupplier.Selected = true;

        //            lock (_RItemSuppModel)
        //            {
        //                _RItemSuppModel.Add(_onesupplier);
        //            }
        //        }

        //        _RItemSuppModel = _RItemSuppModel.OrderByDescending(x => x.Selected).ToList();

        //        RMM.RItemSuppModel = _RItemSuppModel;

        //        RMM.Title = _RawMaterial.Title;
        //        return View("CreateRM", RMM);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json("EditRM " + ex.Message);
        //    }
        //}

        //[HttpPost, ActionName("EditRM")]
        //public async Task<IActionResult> EditRM(RawMaterialModel RMM, IFormFile file)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            RitemMaster _RawMaterial = new RitemMaster();

        //            if (RMM.RItem_ID == 0)
        //                _RawMaterial.RitemId = _rm.GetNewRMId().Result;
        //            else
        //                _RawMaterial.RitemId = RMM.RItem_ID;

        //            _RawMaterial.Title = RMM.Title;

        //            if (file != null)
        //            {
        //                var webRoot = _env.WebRootPath;
        //                //var uploads = Path.Combine(_env.WebRootPath, "uploads/rmphotos");
        //                string rmfilename = file.FileName;
        //                int pos = file.FileName.LastIndexOf(".");
        //                rmfilename = file.FileName.Substring(0, pos);
        //                rmfilename = file.FileName.Replace(rmfilename, _RawMaterial.RitemId.ToString());
        //                var uploads = Path.Combine("uploads/rmphotos/", rmfilename);
        //                var path = Path.Combine(_env.WebRootPath, uploads);
        //                using (var fileStream = new FileStream(path, FileMode.Create))
        //                {
        //                    await file.CopyToAsync(fileStream);
        //                }
        //                _RawMaterial.Title = uploads;
        //            }

        //            _RawMaterial.Name = RMM.Name;
        //            _RawMaterial.Code = RMM.Code;
        //            _RawMaterial.BelongTo = RMM.Subcat_Id;
        //            _RawMaterial.MasterBelongTo = RMM.Cat_Id;
        //            _RawMaterial.ItOrCat = 1;
        //            _RawMaterial.Finish = RMM.Finish;
        //            _RawMaterial.Colour = RMM.Colour;
        //            _RawMaterial.Shape = RMM.Shape;
        //            _RawMaterial.NickleFree = RMM.NickleFree;

        //            _RawMaterial.SizeName = RMM.SizeName;
        //            _RawMaterial.WireSize = RMM.WireSize;
        //            _RawMaterial.Stone = RMM.Stone;
        //            _RawMaterial.Dim1 = RMM.Dim1;
        //            _RawMaterial.Dim2 = "";
        //            _RawMaterial.Diameter = "";
        //            _RawMaterial.PickThickness = RMM.PickThickness;
        //            _RawMaterial.Punit = RMM.PUnit;
        //            _RawMaterial.PucrhasePrice = 0;
        //            _RawMaterial.CostUnit = RMM.CostUnit;
        //            //_RawMaterial.CostPrice = 0;
        //            _RawMaterial.ConversionFactor = RMM.ConversionFactor;

        //            _RawMaterial.OpStock = 0;
        //            _RawMaterial.MinStock = RMM.Min_Stock;
        //            _RawMaterial.MaxStock = RMM.Max_Stock;
        //            _RawMaterial.Was = RMM.WAS;
        //            _RawMaterial.BomWasPercent = RMM.BOM_WAS_Percent;
        //            _RawMaterial.PanningWay = RMM.PanningWay; ;
        //            _RawMaterial.Stucture = RMM.Stucture; ;
        //            _RawMaterial.Washable = RMM.Washable; ;
        //            _RawMaterial.AverageArea = RMM.AverageArea; ;
        //            _RawMaterial.Selection = RMM.Selection;
        //            _RawMaterial.StrenthTest = RMM.StrenthTest; ;
        //            _RawMaterial.Base = RMM.Base; ;
        //            _RawMaterial.DyedSpe = RMM.DyedSpe; ;
        //            _RawMaterial.EuNorms = RMM.EuNorms; ;

        //            _RawMaterial.MinDeliveryDays = RMM.MinDeliveryDays; ;
        //            _RawMaterial.Dnsity = RMM.Dnsity;
        //            _RawMaterial.SessionYear = Helper.Create_Session();
        //            _RawMaterial.UserId = HttpContext.Session.GetInt32("userid").Value;
        //            _RawMaterial.ColorFast = RMM.ColorFast;
        //            _RawMaterial.Description = RMM.Description;
        //            _RawMaterial.Cloth = RMM.Cloth; ;
        //            _RawMaterial.Metal = RMM.Metal;
        //            _RawMaterial.Quality = RMM.Quality; ;
        //            _RawMaterial.Adhesive = RMM.Adhesive;
        //            _RawMaterial.Cartoon = RMM.Cartoon;
        //            _RawMaterial.FullName = RMM.Name;

        //            _RawMaterial.ActualConversionFactor = RMM.ConversionFactor;
        //            _RawMaterial.RmCode = _rm.GetNewRMCodeNumeric(RMM.Cat_Id).Result;
        //            _RawMaterial.SuppCode = RMM.SuppCode;
        //            _RawMaterial.MonthYear = 0;
        //            _RawMaterial.MaxWastage = RMM.Max_Wastage;

        //            _RawMaterial.Thickness = RMM.Thickness; // "";
        //            _RawMaterial.Design = "";
        //            _RawMaterial.Width = RMM.Width;
        //            _RawMaterial.GSM = RMM.GSM;
        //            _RawMaterial.RMBrandId = RMM.RMBrandId;
        //            _RawMaterial.CreatedDate = RMM.CreatedDate;// DateTime.Now.Date;
        //            _RawMaterial.HSNId = RMM.HSNId;
        //            _RawMaterial.IsCertificateRequiredForPurchase = RMM.CertReqForPuchaseId;

        //            _RawMaterial.RMUpdateOn = DateTime.Now.Date;
        //            _RawMaterial.RMUpdateUser = HttpContext.Session.GetInt32("userid").Value;
        //            _RawMaterial.RMReviewStatus = 0;
        //            _RawMaterial.RMReviewOn = DateTime.Now.Date;
        //            _RawMaterial.RMReviewUser = 0; // HttpContext.Session.GetInt32("userid").Value;

        //            List<RItemSupp> icollection = new List<RItemSupp>();

        //            RItemSupp _RItemSupp;
        //            long ritemsuppid = _rm.GetNewRMSuppId().Result;
        //            for (int i = 0; i < RMM.RItemSuppModel.Count; i++)
        //            {
        //                if (RMM.RItemSuppModel[i].Selected == true)
        //                {
        //                    _RItemSupp = new RItemSupp();
        //                    _RItemSupp.RItem_Id = _RawMaterial.RitemId;
        //                    _RItemSupp.SupplierId = RMM.RItemSuppModel[i].SupplierId;
        //                    _RItemSupp.RItemSuppID = ritemsuppid;
        //                    _RItemSupp.Price = 0;
        //                    _RItemSupp.MinDays = 0;
        //                    _RItemSupp.SuppUnit = RMM.PUnit; //unitid;
        //                    _RItemSupp.SupplierRMCode = "-";
        //                    ritemsuppid++;
        //                    lock (icollection)
        //                    {
        //                        icollection.Add(_RItemSupp);
        //                    }
        //                }
        //            }

        //            _RawMaterial.RItemSupp = icollection;

        //            string result = _rm.PutRM(_RawMaterial).Result;
        //            ModelState.Clear();
        //            return RedirectToAction("RMList");
        //        }
        //        return RedirectToAction("CreateRM");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json("EditRM " + ex.Message);
        //    }
        //}

        public JsonResult PutRMReviewStatus(long ritemid)
        {
            try
            {
                RitemMaster _ritemmaster = new RitemMaster();
                _ritemmaster.RitemId = ritemid;
                _ritemmaster.RMReviewStatus = 1;
                _ritemmaster.RMReviewOn = DateTime.Now.Date;
                _ritemmaster.RMReviewUser = HttpContext.Session.GetInt32("userid").Value;
                return Json(_rm.PutRMReviewStatus(_ritemmaster).Result);
            }
            catch (Exception ex)
            {
                return Json("PutRMReviewStatus " + ex.Message);
            }
        }

        public JsonResult GetSubCategory(int id)
        {
            try
            {
                return Json(_rm.GetSubCategoryList(id).Result);
            }
            catch (Exception ex)
            {
                return Json("GetSubCategory " + ex.Message);
            }
        }

        public JsonResult Get_MaxRMCode(Int16 RM_Cate_ID)
        {
            try
            {
                return Json(_rm.GetNewRMCode(RM_Cate_ID).Result);
            }
            catch (Exception ex)
            {
                return Json("Get_MaxRMCode " + ex.Message);
            }
        }

        public JsonResult GetRMList(int subcatId)
        {
            try
            {
                return Json(_rm.GetRMList(subcatId).Result);
            }
            catch (Exception ex)
            {
                return Json("GetRMList " + ex.Message);
            }
        }

        public JsonResult GetRMImagePath(int ritemid)
        {
            try
            {
                return Json(_rm.GetRMImagePath(ritemid).Result);
            }
            catch (Exception ex)
            {
                return Json("GetRMImagePath " + ex.Message);
            }
        }
        
        [HttpPost, ActionName("MoveRM")]
        public ActionResult MoveRM(List<RitemMaster> RMList)
        {
            try
            {
                string result = "";
                foreach (RitemMaster rm in RMList)
                {
                    rm.RMUpdateUser = HttpContext.Session.GetInt32("userid").Value;
                    rm.RMUpdateOn = DateTime.Now.Date;
                    result = _rm.PutMoveRM(rm).Result;
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json("MoveRM " + ex.Message);
            }
        }

        public ActionResult DeleteRM(int id)
        {
            try
            {
                var result = _rm.Delete(id).Result;
                return Json(result);
                //return RedirectToAction("RMList");
            }
            catch (Exception ex)
            {
                return Json("DeleteRM " + ex.Message);
            }
        }

        public ActionResult RMSupplierPrice()
        {
            try
            {
                RMM = new RawMaterialModel();
                RMM.SupplierList = Helper.ConvertObjectToSelectList(_rm.GetSupplierNameList().Result);
                RMM.RMCategoryList = Helper.ConvertObjectToSelectList(_rm.GetCategoryList().Result);
                RMM.RMSubCategoryList = Helper.FillDropDownList_Empty();
                RMM.RawMaterialList = Helper.FillDropDownList_Empty();
                return View(RMM);
            }
            catch (Exception ex)
            {
                return Json("RMSupplierPrice " + ex.Message);
            }
        }

        //public ActionResult RMSupplierPrice_Partial(long rmcatid, long rmscatid, long ritemid, long supplierid)
        //{
        //    try
        //    {
        //        RMM = new RawMaterialModel();
        //        List<ViewRMSupplier> SupplierList = new List<ViewRMSupplier>();
        //        SupplierList = _rm.RMSupplier(rmcatid, rmscatid, ritemid, supplierid).Result;

        //        RItemSuppModel _RItemSuppModel;
        //        List<RItemSuppModel> icollection = new List<RItemSuppModel>();
        //        for (int i = 0; i < SupplierList.Count; i++)
        //        {
        //            _RItemSuppModel = new RItemSuppModel();
        //            _RItemSuppModel.Min_Days = SupplierList[i].MinDays;
        //            _RItemSuppModel.Rate = SupplierList[i].Price;
        //            _RItemSuppModel.RItemSuppID = SupplierList[i].RItemSuppID;
        //            _RItemSuppModel.SupplierId = SupplierList[i].SupplierId;
        //            _RItemSuppModel.SupplierCode = SupplierList[i].SupplierCode;
        //            _RItemSuppModel.SupplierName = SupplierList[i].SupplierName;
        //            _RItemSuppModel.RMCategory = SupplierList[i].CategoryName;
        //            _RItemSuppModel.RMSubCategory = SupplierList[i].SubCategoryName;
        //            _RItemSuppModel.RItem_Id = SupplierList[i].RItem_Id;
        //            _RItemSuppModel.Full_Name = SupplierList[i].Full_Name;
        //            _RItemSuppModel.SupplierRMCode = SupplierList[i].SupplierRMCode;

        //            //_RItemSuppModel.Name = SupplierList[i].CategoryName + " > " + SupplierList[i].SubCategoryName + " > " + SupplierList[i].Name;

        //            _RItemSuppModel.Code = SupplierList[i].Code;
        //            _RItemSuppModel.CostPrice = SupplierList[i].CostPrice;
        //            _RItemSuppModel.PUnitSName = SupplierList[i].PUnitSName;
        //            _RItemSuppModel.CurrencyName = SupplierList[i].CName;
        //            _RItemSuppModel.SuppUnit = SupplierList[i].SuppUnit;

        //            lock (icollection)
        //            {
        //                icollection.Add(_RItemSuppModel);
        //            }
        //        }

        //        RMM.SuppUnitList = Helper.ConvertObjectToSelectList(_rm.GetUnitDetails("").Result);
        //        RMM.RItemSuppModel = icollection;
        //        return PartialView("_RMSupplierPrice", RMM);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json("RMSupplierPrice_Partial " + ex.Message);
        //    }
        //}

        public ActionResult RMSupplierPrice_Partial(long rmcatid, long rmscatid, long ritemid, long supplierid)
        {
            try
            {
                RMM = new RawMaterialModel();
                List<ViewRMSupplier> SupplierList = new List<ViewRMSupplier>();
                SupplierList = _rm.RMSupplier(rmcatid, rmscatid, ritemid, supplierid).Result;

                RItemSuppModel _RItemSuppModel;
                List<RItemSuppModel> icollection = new List<RItemSuppModel>();
                for (int i = 0; i < SupplierList.Count; i++)
                {
                    _RItemSuppModel = new RItemSuppModel();
                    _RItemSuppModel.Min_Days = SupplierList[i].MinDays;
                    _RItemSuppModel.Rate = SupplierList[i].Price;
                    _RItemSuppModel.RItemSuppID = SupplierList[i].RItemSuppID;
                    _RItemSuppModel.SupplierId = SupplierList[i].SupplierId;
                    _RItemSuppModel.SupplierCode = SupplierList[i].SupplierCode;
                    _RItemSuppModel.SupplierName = SupplierList[i].SupplierName;
                    _RItemSuppModel.RMCategory = SupplierList[i].CategoryName;
                    _RItemSuppModel.RMSubCategory = SupplierList[i].SubCategoryName;
                    _RItemSuppModel.RItem_Id = SupplierList[i].RItem_Id;
                    _RItemSuppModel.Full_Name = SupplierList[i].Full_Name;
                    _RItemSuppModel.SupplierRMCode = SupplierList[i].SupplierRMCode;
                    //_RItemSuppModel.Name = SupplierList[i].CategoryName + " > " + SupplierList[i].SubCategoryName + " > " + SupplierList[i].Name;

                    _RItemSuppModel.Code = SupplierList[i].Code;
                    _RItemSuppModel.CostPrice = SupplierList[i].CostPrice;
                    _RItemSuppModel.PUnitSName = SupplierList[i].PUnitSName;
                    _RItemSuppModel.SuppUnitSName = SupplierList[i].SuppUnitSName;
                    _RItemSuppModel.CurrencyName = SupplierList[i].CName;
                    _RItemSuppModel.SuppUnit = SupplierList[i].SuppUnit;
                    _RItemSuppModel.OldPrice = SupplierList[i].Price;
                    _RItemSuppModel.ConversionFactor = SupplierList[i].ConversionFactor;                  
                    _RItemSuppModel.PUnit = SupplierList[i].PUnit;
                    _RItemSuppModel.CostUnit = SupplierList[i].CostUnit;
                    _RItemSuppModel.CUnitSName = SupplierList[i].CUnitSName;

                    _RItemSuppModel.PCSWeight = SupplierList[i].PCSWeight;
                    lock (icollection)
                    {
                        icollection.Add(_RItemSuppModel);
                    }
                }

                RMM.SuppUnitList = Helper.ConvertObjectToSelectList(_rm.GetUnitDetails("").Result);
                RMM.RItemSuppModel = icollection;
                return PartialView("_RMSupplierPrice", RMM);
            }
            catch (Exception ex)
            {
                return Json("RMSupplierPrice_Partial " + ex.Message);
            }
        }

        //[HttpPost]
        //public JsonResult SaveRMSupplierPrice([FromBody]RawMaterialModel RMM)
        //{
        //    try
        //    {
        //        RitemMaster _RawMaterial = new RitemMaster();
        //        bool status = false;

        //        _RawMaterial.MasterBelongTo = RMM.MasterBelongtTo;

        //        List<RItemSupp> icollection = new List<RItemSupp>();
        //        RItemSupp _RItemSupp;

        //        for (int i = 0; i < RMM.RItemSuppModel.Count; i++)
        //        {
        //            _RItemSupp = new RItemSupp();
        //            _RItemSupp.RItem_Id = RMM.RItemSuppModel[i].RItem_Id;
        //            _RItemSupp.SupplierId = RMM.RItemSuppModel[i].SupplierId;
        //            _RItemSupp.RItemSuppID = RMM.RItemSuppModel[i].RItemSuppID;
        //            _RItemSupp.Price = RMM.RItemSuppModel[i].Price;
        //            _RItemSupp.MinDays = RMM.RItemSuppModel[i].Min_Days;
        //            _RItemSupp.SuppUnit = RMM.RItemSuppModel[i].SuppUnit;
        //            _RItemSupp.SupplierRMCode = RMM.RItemSuppModel[i].SupplierRMCode;

        //            lock (icollection)
        //            {
        //                icollection.Add(_RItemSupp);
        //            }
        //        }
        //        _RawMaterial.RItemSupp = icollection;

        //        string result = _rm.PutRMRate(_RawMaterial).Result;

        //        status = true;
        //        return Json(new { status = status });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json("SaveRMSupplierPrice " + ex.Message);
        //    }
        //}

        [HttpPost]
        public JsonResult SaveRMSupplierPrice([FromBody]RawMaterialModel RMM)
        {
            try
            {
                RitemMaster _RawMaterial = new RitemMaster();

                RitemMaster _rmmaster = new RitemMaster();
                List<RitemMaster> irmlist = new List<RitemMaster>();

                bool status = false;
                string result = "";
                _RawMaterial.MasterBelongTo = RMM.MasterBelongtTo;

                List<RItemSupp> icollection = new List<RItemSupp>();
                List<RMSupplierPriceHistory> rmsupplierpricelist = new List<RMSupplierPriceHistory>();
                RItemSupp _RItemSupp;
                RMSupplierPriceHistory _pricehistory;
                long rmpriceHistoryId = _rm.GetNewRMPriceHistoryId().Result;
                for (int i = 0; i < RMM.RItemSuppModel.Count; i++)
                {
                    _RItemSupp = new RItemSupp();                
                    _RItemSupp.RItem_Id = RMM.RItemSuppModel[i].RItem_Id;
                    _RItemSupp.SupplierId = RMM.RItemSuppModel[i].SupplierId;
                    _RItemSupp.RItemSuppID = RMM.RItemSuppModel[i].RItemSuppID;
                    _RItemSupp.Price = RMM.RItemSuppModel[i].Price;
                    _RItemSupp.MinDays = RMM.RItemSuppModel[i].Min_Days;
                    _RItemSupp.SuppUnit = RMM.RItemSuppModel[i].SuppUnit;
                    _RItemSupp.SupplierRMCode = RMM.RItemSuppModel[i].SupplierRMCode;
                    lock (icollection)
                    {
                        icollection.Add(_RItemSupp);
                    }

                    //update costunit ,purcheunit and conversionfactor
                    _rmmaster = new RitemMaster();
                    _rmmaster.RitemId = RMM.RItemSuppModel[i].RItem_Id;
                    _rmmaster.Punit = RMM.RItemSuppModel[i].PUnit;
                    _rmmaster.CostUnit = RMM.RItemSuppModel[i].CostUnit;
                    _rmmaster.ConversionFactor = RMM.RItemSuppModel[i].ConversionFactor;
                    _rmmaster.ActualPurPrice = RMM.RItemSuppModel[i].PCSWeight;

                    lock (irmlist)
                    {
                        irmlist.Add(_rmmaster);
                    }

                    //Price History
                    if (RMM.RItemSuppModel[i].OldPrice != RMM.RItemSuppModel[i].Price)
                    {
                        _pricehistory = new RMSupplierPriceHistory();
                        _pricehistory.RMPriceHistoryId = rmpriceHistoryId;
                        _pricehistory.RItem_Id = RMM.RItemSuppModel[i].RItem_Id;
                        _pricehistory.SupplierId = RMM.RItemSuppModel[i].SupplierId;
                        _pricehistory.OldPrice = RMM.RItemSuppModel[i].OldPrice;
                        _pricehistory.NewPrice = RMM.RItemSuppModel[i].Price;
                        _pricehistory.UserId = HttpContext.Session.GetInt32("userid").Value;
                        _pricehistory.EntryDate = DateTime.Now.Date; ;
                        _pricehistory.SessionYear = Helper.Create_Session();
                        rmpriceHistoryId++;
                        lock (rmsupplierpricelist)
                        {
                            rmsupplierpricelist.Add(_pricehistory);
                        }
                    }
                }
                _RawMaterial.RItemSupp = icollection;

                result = _rm.PutRMDetails(irmlist).Result;
                result = _rm.PostRMSupplierPriceHistory(rmsupplierpricelist).Result;
                result = _rm.PutRMRate(_RawMaterial).Result;

                status = true;
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                return Json("SaveRMSupplierPrice " + ex.Message);
            }
        }


        public ActionResult RMSupplierPriceHistory_Partial(long rmid, long ritemsuppid)
        {
            try
            {
                return PartialView("_RMSupplierPriceHistory", _rm.GetRMSupplierPriceHistory(rmid, ritemsuppid).Result);
            }
            catch (Exception ex)
            {
                return Json("RMSupplierPriceHistory_Partial " + ex.Message);
            }
        }

        public ActionResult RMSupplierPriceHistoryList()
        {
            try
            {
                RMM = new RawMaterialModel();

                RMM.SupplierList = Helper.ConvertObjectToSelectList(_rm.GetSupplierNameList().Result);
                RMM.CreateDateFrom = DateTime.Now.Date;
                RMM.CreateDateTo = DateTime.Now.Date;

                return View(RMM);
            }
            catch (Exception ex)
            {
                return Json("RMSupplierPriceHistoryList " + ex.Message);
            }
        }
        public ActionResult GetRMSupplierPriceHistory_Partial(RMSearch rms)
        {
            try
            {
                var data = _rm.GetRMSupplierPriceHistoryList(rms).Result;
                return PartialView("_RMSupplierPriceHistory", data);
            }
            catch (Exception ex)
            {
                return Json("GetRMSupplierPriceHistory_Partial " + ex.Message);
            }
        }


    public ActionResult RMSupplier()
        {
            try
            {
                RMM = new RawMaterialModel();
                //RMM.SupplierList = Helper.ConvertObjectToSelectList(_rm.GetSupplierNameList().Result);
                RMM.RMCategoryList = Helper.ConvertObjectToSelectList(_rm.GetCategoryList().Result);
                RMM.RMSubCategoryList = Helper.FillDropDownList_Empty();
                RMM.RawMaterialList = Helper.FillDropDownList_Empty();
                return View(RMM);
            }
            catch (Exception ex)
            {
                return Json("RMSupplier " + ex.Message);
            }
        }

        public ActionResult SupplierRM()
        {
            try
            {
                RMM = new RawMaterialModel();
                RMM.SupplierList = Helper.ConvertObjectToSelectList(_rm.GetSupplierNameList().Result);
                //RMM.RMCategoryList = Helper.ConvertObjectToSelectList(_rm.GetCategoryList().Result);
                //RMM.RMSubCategoryList = Helper.FillDropDownList_Empty();
                //RMM.RawMaterialList = Helper.FillDropDownList_Empty();
                return View(RMM);
            }
            catch (Exception ex)
            {
                return Json("SupplierRM " + ex.Message);
            }
        }

        public ActionResult RMSuppNSuppRM_Partial(long rmcatid, long rmscatid, long ritemid, long supplierid)
        {
            try
            {
                RMM = new RawMaterialModel();
                List<ViewRMSupplier> SupplierList = new List<ViewRMSupplier>();
                SupplierList = _rm.RMSupplier(rmcatid, rmscatid, ritemid, supplierid).Result;

                RItemSuppModel _RItemSuppModel;
                List<RItemSuppModel> icollection = new List<RItemSuppModel>();
                for (int i = 0; i < SupplierList.Count; i++)
                {
                    _RItemSuppModel = new RItemSuppModel();
                    _RItemSuppModel.Min_Days = SupplierList[i].MinDays;
                    _RItemSuppModel.Rate = SupplierList[i].Price;
                    _RItemSuppModel.RItemSuppID = SupplierList[i].RItemSuppID;
                    _RItemSuppModel.SupplierId = SupplierList[i].SupplierId;
                    _RItemSuppModel.SupplierCode = SupplierList[i].SupplierCode;
                    _RItemSuppModel.SupplierName = SupplierList[i].SupplierName;
                    _RItemSuppModel.RMCategory = SupplierList[i].CategoryName;
                    _RItemSuppModel.RMSubCategory = SupplierList[i].SubCategoryName;
                    _RItemSuppModel.RItem_Id = SupplierList[i].RItem_Id;
                    _RItemSuppModel.Full_Name = SupplierList[i].Full_Name;
                    //_RItemSuppModel.Name = SupplierList[i].CategoryName + " > " + SupplierList[i].SubCategoryName + " > " + SupplierList[i].Name;

                    _RItemSuppModel.Code = SupplierList[i].Code;
                    _RItemSuppModel.CostPrice = SupplierList[i].CostPrice;
                    _RItemSuppModel.PUnitSName = SupplierList[i].PUnitSName;
                    _RItemSuppModel.CurrencyName = SupplierList[i].CName;
                    _RItemSuppModel.SuppUnit = SupplierList[i].SuppUnit;
                    _RItemSuppModel.SuppUnitSName = SupplierList[i].SuppUnitSName;

                    lock (icollection)
                    {
                        icollection.Add(_RItemSuppModel);
                    }
                }

                RMM.SuppUnitList = Helper.ConvertObjectToSelectList(_rm.GetUnitDetails("").Result);
                RMM.RItemSuppModel = icollection;
                return PartialView("_RMSuppNSuppRM", RMM);
            }
            catch (Exception ex)
            {
                return Json("RMSuppNSuppRM_Partial " + ex.Message);
            }
        }

        public ActionResult RMCostPrice()
        {
            try
            {
                RMM = new RawMaterialModel();
                RMM.RMCategoryList = Helper.ConvertObjectToSelectList(_rm.GetCategoryList().Result);
                RMM.RMSubCategoryList = Helper.FillDropDownList_Empty();
                RMM.RawMaterialList = Helper.FillDropDownList_Empty();
                return View(RMM);
            }
            catch (Exception ex)
            {
                return Json("RMCostPrice " + ex.Message);
            }
        }

        public ActionResult RMCostPrice_Partial(long rmcatid, long rmscatid, long ritemid)
        {
            try
            {
                List<ViewRItemShow> viewritemshow = new List<ViewRItemShow>();
                //viewritemshow = _rm.GetRitemByCatid(catid).Result;
                viewritemshow = _rm.GetRitemRecord(rmcatid, rmscatid, ritemid).Result;

                List<RawMaterialModel> RMMList = new List<RawMaterialModel>();
                for (int i = 0; i < viewritemshow.Count; i++)
                {
                    RMM = new RawMaterialModel();
                    RMM.RItem_ID = viewritemshow[i].RitemId;
                    RMM.Code = viewritemshow[i].Code;
                    RMM.Name = viewritemshow[i].CategoryName + " > " + viewritemshow[i].SubCategoryName + " > " + viewritemshow[i].Name;
                    RMM.CostPrice = viewritemshow[i].CostPrice;
                    RMM.CostUnitSName = viewritemshow[i].CostUnitSName;
                    RMM.ConversionFactor = viewritemshow[i].ConversionFactor;
                    RMM.PUnitSName = viewritemshow[i].PUnitSName;

                    RMM.RMSuppMaxPrice = viewritemshow[i].RMSuppMaxPrice;
                    lock (RMMList)
                    {
                        RMMList.Add(RMM);
                    }
                }
                return PartialView("_RMCostPrice", RMMList);
            }
            catch (Exception ex)
            {
                return Json("RMCostPrice_Partial " + ex.Message);
            }
        }

        [HttpPost]
        public JsonResult SaveRMCostPrice([FromBody]List<RawMaterialModel> RMMList)
        {
            try
            {
                List<RitemMaster> _RawMateriallist = new List<RitemMaster>();
                bool status = false;

                RitemMaster ritemMaster;

                for (int i = 0; i < RMMList.Count; i++)
                {
                    ritemMaster = new RitemMaster();
                    ritemMaster.RitemId = RMMList[i].RItem_ID;
                    ritemMaster.CostPrice = RMMList[i].CostPrice;
                    lock (_RawMateriallist)
                    {
                        _RawMateriallist.Add(ritemMaster);
                    }
                }

                string result = _rm.PutRMCostPrice(_RawMateriallist).Result;

                status = true;
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                return Json("SaveRMCostPrice " + ex.Message);
            }
        }

        #region RMForProduct
        public ActionResult RMForProduct()
        {
            try
            {
                RMM = new RawMaterialModel();
                //RMM.RMCategoryList = Helper.ConvertObjectToSelectList(_rm.GetCategoryList().Result);
                //RMM.RMSubCategoryList = Helper.FillDropDownList_Empty();
                RMM.RawMaterialList = Helper.ConvertObjectToSelectList(_rm.GetRMList().Result);
                return View(RMM);
            }
            catch (Exception ex)
            {
                return Json("RMForProduct " + ex.Message);
            }
        }

        public ActionResult RMForProduct_Partial(long ritemid)
        {
            try
            {
                RMM = new RawMaterialModel();
                List<ViewShowCosting> SupplierList = new List<ViewShowCosting>();
                SupplierList = _rm.RMForProduct(ritemid).Result;

                RItemSuppModel _RItemSuppModel;
                List<RItemSuppModel> icollection = new List<RItemSuppModel>();
                for (int i = 0; i < SupplierList.Count; i++)
                {
                    _RItemSuppModel = new RItemSuppModel();
                    _RItemSuppModel.ProductCategory = SupplierList[i].ProductCategoryName;
                    _RItemSuppModel.ProductSubCategory = SupplierList[i].ProductSubCategoryName;
                    _RItemSuppModel.ProductName = SupplierList[i].Name;
                    _RItemSuppModel.ProductCode = SupplierList[i].Code;
                    _RItemSuppModel.PhotoPath = SupplierList[i].PhotoPath;
                    _RItemSuppModel.FItem_Id = SupplierList[i].FitemID;
                    _RItemSuppModel.CostingID = SupplierList[i].CostingID;
                    _RItemSuppModel.ProductSizeName = SupplierList[i].ProductSizeName;

                    lock (icollection)
                    {
                        icollection.Add(_RItemSuppModel);
                    }
                }

                RMM.RItemSuppModel = icollection;
                return PartialView("_RMForProduct", RMM);
            }
            catch (Exception ex)
            {
                return Json("RMForProduct_Partial " + ex.Message);
            }
        }

        public ActionResult RMSizeForProduct_Partial(long ritemid)
        {
            try
            {
                RMM = new RawMaterialModel();
                List<ViewShowRMSizeCosting> rmsizecosting = new List<ViewShowRMSizeCosting>();
                rmsizecosting = _rm.RMSizeForProduct(ritemid).Result;
                return PartialView("_RMSizeForProduct", rmsizecosting);
            }
            catch (Exception ex)
            {
                return Json("RMForProduct_Partial " + ex.Message);
            }
        }

        [HttpPost, ActionName("PutRMinProduct")]
        public ActionResult PutRMinProduct(List<CostingSearch> obj)
        {
            try
            {
                string result = "";
                result = _rm.PutRMinProduct(obj).Result;
                //foreach (CostingSearch costingsearch in obj)
                //{
                //    result = _rm.PutRMinProduct(costingsearch).Result;
                //}
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json("UpdateRMinProduct " + ex.Message);
            }
        }

        [HttpPost, ActionName("PutRMinProductSizeWise")]
        public ActionResult PutRMinProductSizeWise(List<CostingSearch> obj)
        {
            try
            {
                string result = "";
                result = _rm.PutRMinProductSizeWise(obj).Result;
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json("UpdateRMSizeWise " + ex.Message);
            }
        } 
        #endregion

        public ActionResult RMMinimumLevel()
        {
            try
            {
                RMM = new RawMaterialModel();
                return View(RMM);
            }
            catch (Exception ex)
            {
                return Json("RMMinimumLevel " + ex.Message);
            }
        }

        public ActionResult RMMinimumLevel_Partial()
        {
            try
            {
                RMM = new RawMaterialModel();
                List<ViewBelowMinimumLevel> MinimumLevel = new List<ViewBelowMinimumLevel>();
                MinimumLevel = _rm.RMMinimumLevel().Result;

                RItemSuppModel _RItemSuppModel;
                List<RItemSuppModel> icollection = new List<RItemSuppModel>();
                for (int i = 0; i < MinimumLevel.Count; i++)
                {
                    _RItemSuppModel = new RItemSuppModel();
                    _RItemSuppModel.MiniLevelCode = MinimumLevel[i].Code;
                    _RItemSuppModel.MiniLevelRMName = MinimumLevel[i].Full_Name;
                    _RItemSuppModel.MiniLevelStock = MinimumLevel[i].CurrentStock;
                    _RItemSuppModel.MiniLevelUnit = MinimumLevel[i].CostUnitSName;
                    _RItemSuppModel.MiniLevel = MinimumLevel[i].Min_Stock;
                    _RItemSuppModel.PhotoPath = MinimumLevel[i].Title;

                    lock (icollection)
                    {
                        icollection.Add(_RItemSuppModel);
                    }
                }

                RMM.RItemSuppModel = icollection;
                return PartialView("_RMMinimumLevel", RMM);
            }
            catch (Exception ex)
            {
                return Json("RMMinimumLevel_Partial " + ex.Message);
            }
        }

        public ActionResult MinimumRMPrint()
        {
            try
            {
                ReportClass rptH = new ReportClass();
                IEnumerable<ViewBelowMinimumLevel> data = _rm.RMMinimumLevel().Result;
                DataTable table = new DataTable();
                table.TableName = "ViewBelowMinimumLevel";
                using (var reader = ObjectReader.Create(data))
                {
                    table.Load(reader);
                }

                table.WriteXml(Path.Combine(_env.WebRootPath, "crystal/xml/ViewBelowMinimumLevel.xml"), XmlWriteMode.WriteSchema);
                rptH.FileName = Path.Combine(_env.WebRootPath, "crystal/rpt/MinimumRMPrint.rpt");

                rptH.Load();
                rptH.SetDataSource(table);
                Stream stream = rptH.ExportToStream(ExportFormatType.PortableDocFormat);
                return File(stream, "application/pdf");
            }
            catch (Exception ex)
            {
                return Json("MinimumRMPrint " + ex.Message);
            }
        }

        public ActionResult UnusedRMList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Json("UnusedRMList " + ex.Message);
            }
        }

        public ActionResult UnusedRMList_Partial()
        {
            try
            {
                List<ViewUnusedRMList> UnusedRm = new List<ViewUnusedRMList>();
                UnusedRm = _rm.GetUnusedRmList().Result;

                RItemSuppModel _RISupp;
                List<RItemSuppModel> icollection = new List<RItemSuppModel>();
                for (int i = 0; i < UnusedRm.Count; i++)
                {
                    _RISupp = new RItemSuppModel();
                    _RISupp.ProductCategory = UnusedRm[i].CategoryName;
                    _RISupp.ProductSubCategory = UnusedRm[i].SubCategoryName;
                    _RISupp.Code = UnusedRm[i].Code;
                    _RISupp.Full_Name = UnusedRm[i].Full_Name;
                    _RISupp.PhotoPath = UnusedRm[i].Title;

                    lock (icollection)
                    {
                        icollection.Add(_RISupp);
                    }
                }
                return PartialView("_UnusedRMList", icollection);
            }
            catch (Exception ex)
            {
                return Json("UnusedRMList_Partial " + ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Upload(long id)
        {

            try
            {
                List<ViewRMFile> _list = new List<ViewRMFile>();
                _list = _rm.GetRMFileRecord(id).Result;
                RitemMaster _RawMaterial = new RitemMaster();
                _RawMaterial = _rm.Get(Convert.ToInt32(id)).Result;
                RMFileModel _rmfilemodel;
                List<RMFileModel> _listmodel = new List<RMFileModel>();

                for (int i = 0; i < _list.Count; i++)
                {
                    _rmfilemodel = new RMFileModel();
                    _rmfilemodel.RMFileId = _list[i].RMFileId;
                    _rmfilemodel.RitemId = _list[i].RItem_Id;
                    _rmfilemodel.FileLocation = _list[i].FileLocation;
                    _rmfilemodel.FileName = _list[i].FileName;
                    _rmfilemodel.UploadDate = _list[i].UploadDate;
                    _rmfilemodel.IsPhoto = _list[i].IsPhoto;
                    _rmfilemodel.DocumentTypeName = _list[i].DocumentTypeName;
                    _rmfilemodel.SupplierName = _list[i].SupplierName;
                    _rmfilemodel.SupplierCode = _list[i].SupplierCode;
                    _rmfilemodel.ValidFrom = _list[i].ValidFrom;
                    _rmfilemodel.ValidTo = _list[i].ValidTo;
                    _rmfilemodel.AlertInDays = _list[i].AlertInDays;
                    _rmfilemodel.Remarks = _list[i].Remarks;
                    _listmodel.Add(_rmfilemodel);
                }

                RawMaterialModel _rmmodel = new RawMaterialModel();
                _rmmodel.Code = _RawMaterial.Code;
                _rmmodel.Name = _RawMaterial.Name;
                _rmmodel.RItem_ID = id;
                _rmmodel.RMFileModel = _listmodel;
                _rmmodel.FileUploadMessage = null;
                _rmmodel.ValidFrom = DateTime.Now.Date;
                _rmmodel.ValidTo = DateTime.Now.Date;
                _rmmodel.DocumentTypeList = Helper.ConvertObjectToSelectList(_rm.RMDocumentTypeList(id).Result);
                _rmmodel.SupplierList = Helper.ConvertObjectToSelectList(_rm.GetSupplierNameList().Result);
                return PartialView("UploadRMFiles", _rmmodel);
            }
            catch (Exception ex)
            {
                return Json("upload document " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upload(RawMaterialModel _rmmodel, IFormFile file)
        {
            try
            {
                RMFile _rmfile = new RMFile();
                _rmfile.RMFileId = _rm.GetNewId().Result;

                string filename = file.FileName;
                var Directory = Path.Combine(_env.WebRootPath, "uploads/rmfile/" + _rmmodel.RItem_ID);

                if (!System.IO.Directory.Exists(Directory))
                {
                    System.IO.Directory.CreateDirectory(Directory);
                }

                var FileLocation = Path.Combine("uploads/rmfile/" + _rmmodel.RItem_ID, filename);
                var UploadPath = Path.Combine(Directory, filename);
                using (var fileStream = new FileStream(UploadPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                _rmfile.RItem_Id = _rmmodel.RItem_ID;
                _rmfile.FileLocation = FileLocation;
                _rmfile.FileName = filename;
                _rmfile.UploadDate = DateTime.Now.Date;
                _rmfile.IsPhoto = 1;
                _rmfile.UserId = HttpContext.Session.GetInt32("userid").Value;
                _rmfile.SessionYear = Helper.Create_Session();
                _rmfile.DocumentTypeId = _rmmodel.DocumentTypeId;

                _rmfile.SupplierId = _rmmodel.SupplierId;
                _rmfile.ValidFrom = _rmmodel.ValidFrom;
                _rmfile.ValidTo = _rmmodel.ValidTo;
                _rmfile.AlertInDays = _rmmodel.AlertInDays;
                _rmfile.Remarks = _rmmodel.Remarks;
                _rmfile.IsReveiew = 0;
                _rmfile.ReviewDate = DateTime.Now.Date;
                _rmfile.ReviewStatus = 0;
                string result = _rm.PostRMFile(_rmfile).Result;
                return RedirectToAction("Upload", "RM", new RouteValueDictionary(new { Id = _rmmodel.RItem_ID }));
            }
            catch (Exception ex)
            {
                return Json("file attach " + ex.Message);
            }
        }

        public ActionResult DeleteRMFile(long id, long rmid)
        {
            try
            {
                string filename = _rm.RMFileName(id).Result;
                var status = _rm.DeleteRMFile(id).Result;
                if (status == 1)
                {
                    var path = Path.Combine(_env.WebRootPath, filename);
                    System.IO.File.Delete(path);
                }
                return Json(status);
            }
            catch (Exception ex)
            {
                return Json("DeleteRMFile " + ex.Message);
            }
        }

        public JsonResult GetSubCategoryDetails(int id)
        {
            try
            {
                return Json(_rm.GetSubCategoryDetails(id).Result);
            }
            catch (Exception ex)
            {
                return Json("GetSubCategoryDetails " + ex.Message);
            }
        }

        public ActionResult RMPValueList(long catid)
        {
            try
            {
                RMM = new RawMaterialModel();
                //RMM.RMPValueList = Helper.FillDropDownList_Empty();
                List<ViewRMPropertiesMapping> rmmapping = _rm.GetCatMapProperties(catid).Result;


                List<RMPropertiesModel> rmpropertieslist = new List<RMPropertiesModel>();
                RMPropertiesModel _rmp;

                for (var i = 0; i < rmmapping.Count; i++)
                {
                    _rmp = new RMPropertiesModel();
                    _rmp.RMPropertiesID = rmmapping[i].RMPropertiesID;
                    _rmp.RMPropertiesName = rmmapping[i].RMPropertiesName;
                    _rmp.RMPropertiesIsMaster = rmmapping[i].RMPropertiesIsMaster;
                    _rmp.IsRequired = rmmapping[i].IsRequired;
                    _rmp.RMPValueList = Helper.ConvertObjectToSelectList(_rm.GetRMPropertiesValueList(rmmapping[i].RMPropertiesID).Result);
                    lock (rmpropertieslist)
                    {
                        rmpropertieslist.Add(_rmp);
                    }

                }

                RMM.RMPropertiesModel = rmpropertieslist;
                return View("_RMPCatValues", RMM);
            }
            catch (Exception ex)
            {
                return Json("_RMPCatValues " + ex.Message);
            }
        }

        public ActionResult EdidRMChild(long rmid, long catid)
        {
            try
            {
                RMM = new RawMaterialModel();
                //RMM.RMPValueList = Helper.FillDropDownList_Empty();
                List<ViewRMChild> rmchild = _rm.GetRMChild(rmid).Result;

                List<ViewRMPropertiesMapping> rmmapping = _rm.GetCatMapProperties(catid).Result;

                List<RMPropertiesModel> rmpropertieslist = new List<RMPropertiesModel>();
                RMPropertiesModel _rmp;
                if (rmchild != null)
                {
                    for (var i = 0; i < rmchild.Count; i++)
                    {
                        _rmp = new RMPropertiesModel();
                        _rmp.RMPropertiesID = rmchild[i].RMPropertiesID;
                        _rmp.RMPropertiesName = rmchild[i].RMPropertiesName;
                        _rmp.RMPropertiesIsMaster = rmchild[i].RMPropertiesIsMaster;
                        _rmp.RMPValueList = Helper.ConvertObjectToSelectList(_rm.GetRMPropertiesValueList(rmchild[i].RMPropertiesID).Result);
                        _rmp.RMPropertiesValueID = rmchild[i].RMPropertiesValueID;
                        _rmp.RMPValue = rmchild[i].RMPValue;
                        lock (rmpropertieslist)
                        {
                            rmpropertieslist.Add(_rmp);
                        }
                    }
                }
                else
                {
                    for (var i = 0; i < rmmapping.Count; i++)
                    {
                        _rmp = new RMPropertiesModel();
                        _rmp.RMPropertiesID = rmmapping[i].RMPropertiesID;
                        _rmp.RMPropertiesName = rmmapping[i].RMPropertiesName;
                        _rmp.RMPropertiesIsMaster = rmmapping[i].RMPropertiesIsMaster;
                        _rmp.IsRequired = rmmapping[i].IsRequired;
                        _rmp.RMPValueList = Helper.ConvertObjectToSelectList(_rm.GetRMPropertiesValueList(rmmapping[i].RMPropertiesID).Result);
                        lock (rmpropertieslist)
                        {
                            rmpropertieslist.Add(_rmp);
                        }
                    }
                }
                RMM.RMPropertiesModel = rmpropertieslist;
                return View("_RMPCatValues", RMM);
            }
            catch (Exception ex)
            {
                return Json("_RMPCatValues " + ex.Message);
            }

            // Refresh Button
        }

        public JsonResult RefreshColorList()
        {
            try
            {
                return Json(_rm.GetColorDetails(string.Empty).Result);
            }
            catch (Exception ex)
            {
                return Json("CreateRM " + ex.Message);
            }
        }

        public JsonResult RefreshSizeList()
        {
            try
            {
                return Json(_rm.GetSizeDetails(string.Empty).Result);
            }
            catch (Exception ex)
            {
                return Json("CreateRM " + ex.Message);
            }
        }

        public JsonResult RefreshUnitList()
        {
            try
            {
                return Json(_rm.GetUnitDetails(string.Empty).Result);
            }
            catch (Exception ex)
            {
                return Json("CreateRM " + ex.Message);
            }
        }

        public JsonResult RefreshsnList()
        {
            try
            {
                return Json(_rm.GetHSNList().Result);
            }
            catch (Exception ex)
            {
                return Json("RefreshsnList " + ex.Message);
            }
        }

        public JsonResult RefreshCategory() 
        {
            try
            {
                return Json(_rm.GetCategoryList().Result);
            }
            catch (Exception ex)
            {
                return Json("RefreshCategory " + ex.Message);
            }
        }


    }
}

