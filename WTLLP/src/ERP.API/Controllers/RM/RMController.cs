using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using ERP.Domain.Entities;
using System.Collections.Generic;

namespace ERP.Api
{
    [Route("api/[controller]")]
    public class RMController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;

        public RMController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<RitemMaster> Get()
        {
            //return _context.RitemMaster.AsQueryable();
            return _context.RitemMaster.Where(x => x.Code != null).OrderByDescending(x => x.RitemId).AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                //return Ok(_context.RitemMaster.Where(x => x.RitemId == id));
                return Ok(_context.RitemMaster.Where(x => x.RitemId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("RItemShow/{id}")]
        public IActionResult RItemShow(int id)
        {
            try
            {
                return Ok(_context.ViewRItemShow.Where(x => x.RitemId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewRMCode/{categoryId}")]
        public IActionResult GetNewRMCode(int categoryId)
        {
            try
            {
                long RM_code = 0;
                var lastRItemMaster = _context.RitemMaster.Where(x => x.MasterBelongTo == categoryId).OrderBy(x => x.RmCode).LastOrDefault();
                //SQLDBUtility.GetValue<Int64>("SELECT MAX(RM_Code) FROM RItem_Master Where MasterBelongTo =" + RM_Cate_ID + " ", CommandType.Text, ref RM_code);
                RM_code = (lastRItemMaster == null ? 0 : lastRItemMaster.RmCode) + 1;
                string Raw_material_code = "";
                string Shortcode = "";
                switch (categoryId)
                {
                    case 1:
                        Shortcode = "LE - ";
                        break;
                    case 2:
                        Shortcode = "FT - ";
                        break;
                    case 3:
                        Shortcode = "OT - ";
                        break;
                    case 4:
                        Shortcode = "FB - ";
                        break;
                    case 5:
                        Shortcode = "PA - ";
                        break;
                    case 6:
                        Shortcode = "PC - ";
                        break;
                    case 7:
                        Shortcode = "Th - ";
                        break;
                }
                Raw_material_code = Shortcode + RM_code.ToString();
                return Ok(Raw_material_code);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetRMImagePath/{ritemid}")]
        public IActionResult GetRMImagePath(long ritemid)
        {
            try
            {
                string title = "";
                var rawmaterial = _context.RitemMaster.Find(ritemid);
                title = rawmaterial.Title;
                return Ok(title);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewRMCodeNumeric/{categoryId}")]
        public IActionResult GetNewRMCodeNumeric(int categoryId)
        {
            try
            {
                long RM_code = 0;
                var lastRItemMaster = _context.RitemMaster.Where(x => x.MasterBelongTo == categoryId).OrderBy(x => x.RmCode).LastOrDefault();
                RM_code = (lastRItemMaster == null ? 0 : lastRItemMaster.RmCode) + 1;
                return Ok(RM_code);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewRMId")]
        public IActionResult GetNewRMId()
        {
            try
            {
                long RitemId = 0;
                var lastRItemMaster = _context.RitemMaster.OrderBy(x => x.RitemId).LastOrDefault();
                RitemId = (lastRItemMaster == null ? 0 : lastRItemMaster.RitemId) + 1;
                return Ok(RitemId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewRMSuppId")]
        public IActionResult GetNewRMSuppId()
        {
            try
            {
                long RItemSupp_ID = 0;
                var lastRItemSupp = _context.RItemSupp.OrderBy(x => x.RItemSuppID).LastOrDefault();
                RItemSupp_ID = (lastRItemSupp == null ? 0 : lastRItemSupp.RItemSuppID) + 1;
                return Ok(RItemSupp_ID);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("RMCategory")]
        public IActionResult GetCategory()
        {
            try
            {
                object categories;
                categories = _context.RMCategory
                                    .OrderBy(c => c.CategoryName)
                                    .Select(x => new { Id = x.CategoryID, Value = x.CategoryName }).ToList();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("RMSubCategory/{categoryId}")]
        public IActionResult GetSubCategory(long categoryId)
        {
            try
            {
                object categories;
                categories = _context.RMSubCategory
                                    .Where(x => x.CategoryID == categoryId)
                                    .OrderBy(c => c.SubCategoryName)
                                    .Select(x => new { Id = x.SubCategoryID, Value = x.SubCategoryName }).ToList();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("SubCategoryList")]
        public IActionResult GetSubCategoryList(string search)
        {
            try
            {
                object categories;
                categories = _context.RMSubCategory
                                    .Where(c => c.SubCategoryName.Contains(search))
                                    .OrderBy(c => c.SubCategoryName)
                                    .Select(x => new { Id = x.SubCategoryID, Value = x.SubCategoryName }).ToList();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetRMList/{subcatId}")]
        public IActionResult GetRMList(int subcatId)
        {
            try
            {
                object categories;
                categories = _context.RitemMaster
                                    .Where(x => x.BelongTo == subcatId)
                                    .OrderBy(c => c.Name)
                                    .Select(x => new { Id = x.RitemId, Value = x.Code + " " + x.Name }).ToList();

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("FinishMetalDetails")]
        public IActionResult GetFinishMetalDetails()
        {
            try
            {
                var finishMetalDetails = _context.FinishMetalDetails
                                            .OrderBy(c => c.Mfname)
                                            .Select(x => new { Id = x.Mfid, Value = x.Mfname });
                return Ok(finishMetalDetails);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("ThicknessDetails")]
        public IActionResult GetThicknessDetails()
        {
            try
            {
                var thicknessDetails = _context.ThicknessDetails
                                            .OrderBy(c => c.Thname)
                                            .Select(x => new { Id = x.Thid, Value = x.Thname });
                return Ok(thicknessDetails);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("showcolor")]
        public IActionResult ShowColor(string color)
        {
            try
            {
                object colorDetails;
                if (string.IsNullOrEmpty(color))
                {
                    colorDetails = _context.ColorDetails
                                        .OrderBy(c => c.Clname)
                                        .Select(x => new { Id = x.Clid, Value = x.Clname });
                }
                else
                {
                    colorDetails = _context.ColorDetails
                                        .Where(x => x.Clname.Contains(color))
                                        .OrderBy(c => c.Clname)
                                        .Select(x => new { Id = x.Clid, Value = x.Clname });
                }
                return Ok(colorDetails);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("showshape")]
        public IActionResult ShowShape(string shape)
        {
            try
            {
                object shapeDetails;
                if (string.IsNullOrEmpty(shape))
                {
                    shapeDetails = _context.ShapeDetails
                                        .OrderBy(c => c.ShapeName)
                                        .Select(x => new { Id = x.ShapeId, Value = x.ShapeName });
                }
                else
                {
                    shapeDetails = _context.ShapeDetails
                                        .Where(x => x.ShapeName.Contains(shape))
                                        .OrderBy(c => c.ShapeName)
                                        .Select(x => new { Id = x.ShapeId, Value = x.ShapeName });
                }
                return Ok(shapeDetails);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("showsize")]
        public IActionResult ShowSize(string size)
        {
            try
            {
                object sizeDetails;
                if (string.IsNullOrEmpty(size))
                {
                    sizeDetails = _context.SizeDetails
                                        .OrderBy(c => c.SizeName)
                                        .Select(x => new { Id = x.SizeId, Value = x.SizeName });
                }
                else
                {
                    sizeDetails = _context.SizeDetails
                                        .Where(x => x.SizeName.Contains(size))
                                        .OrderBy(c => c.SizeName)
                                        .Select(x => new { Id = x.SizeId, Value = x.SizeName });
                }
                return Ok(sizeDetails);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("fillmetal")]
        public IActionResult FillMetal(string metal)
        {
            try
            {
                object metalDetails;
                if (string.IsNullOrEmpty(metal))
                {
                    metalDetails = _context.MetalPartDetails
                                        .OrderBy(c => c.MepName)
                                        .Select(x => new { Id = x.MepId, Value = x.MepName });
                }
                else
                {
                    metalDetails = _context.MetalPartDetails
                                        .Where(x => x.MepName.Contains(metal))
                                        .OrderBy(c => c.MepName)
                                        .Select(x => new { Id = x.MepId, Value = x.MepName });
                }
                return Ok(metalDetails);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("showstone")]
        public IActionResult ShowStone(string stone)
        {
            try
            {
                object stoneDetails;
                if (string.IsNullOrEmpty(stone))
                {
                    stoneDetails = _context.Stone
                                        .OrderBy(c => c.StoneName)
                                        .Select(x => new { Id = x.Sid, Value = x.StoneName });
                }
                else
                {
                    stoneDetails = _context.Stone
                                        .Where(x => x.StoneName.Contains(stone))
                                        .OrderBy(c => c.StoneName)
                                        .Select(x => new { Id = x.Sid, Value = x.StoneName });
                }
                return Ok(stoneDetails);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("fillunit")]
        public IActionResult FillUnit(string unit)
        {
            try
            {
                object unitDetails;
                if (string.IsNullOrEmpty(unit))
                {
                    unitDetails = _context.UnitDetails
                                        .OrderBy(c => c.Uname)
                                        .Select(x => new { Id = x.Uid, Value = x.Uname });
                }
                else
                {
                    unitDetails = _context.UnitDetails
                                        .Where(x => x.Uname.Contains(unit))
                                        .OrderBy(c => c.Uname)
                                        .Select(x => new { Id = x.Uid, Value = x.Uname });
                }
                return Ok(unitDetails);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("fillcartoon")]
        public IActionResult FillCartoon(string cartoon)
        {
            try
            {
                object cartonDetails;
                if (string.IsNullOrEmpty(cartoon))
                {
                    cartonDetails = _context.CartonDetails
                                        .OrderBy(c => c.Dimension)
                                        .Select(x => new { Id = x.CartonId, Value = x.Dimension });
                }
                else
                {
                    cartonDetails = _context.CartonDetails
                                        .Where(x => x.Dimension.Contains(cartoon))
                                        .OrderBy(c => c.Dimension)
                                        .Select(x => new { Id = x.CartonId, Value = x.Dimension });
                }
                return Ok(cartonDetails);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("fillquality")]
        public IActionResult FillQuality(string quality)
        {
            try
            {
                object qualityDetails;
                if (string.IsNullOrEmpty(quality))
                {
                    qualityDetails = _context.QualityDetails
                                        .OrderBy(c => c.QualityName)
                                        .Select(x => new { Id = x.QualityId, Value = x.QualityName });
                }
                else
                {
                    qualityDetails = _context.QualityDetails
                                        .Where(x => x.QualityName.Contains(quality))
                                        .OrderBy(c => c.QualityName)
                                        .Select(x => new { Id = x.QualityId, Value = x.QualityName });
                }
                return Ok(qualityDetails);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("fillstructure")]
        public IActionResult FillStructure(string structure)
        {
            try
            {
                object structureDetails;
                if (string.IsNullOrEmpty(structure))
                {
                    structureDetails = _context.StructureDetails
                                        .OrderBy(c => c.Tsname)
                                        .Select(x => new { Id = x.Tsid, Value = x.Tsname });
                }
                else
                {
                    structureDetails = _context.StructureDetails
                                        .Where(x => x.Tsname.Contains(structure))
                                        .OrderBy(c => c.Tsname)
                                        .Select(x => new { Id = x.Tsid, Value = x.Tsname });
                }
                return Ok(structureDetails);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("filladhesive")]
        public IActionResult FillAdhesive(string adhesive)
        {
            try
            {
                object adhesiveDetails;
                if (string.IsNullOrEmpty(adhesive))
                {
                    adhesiveDetails = _context.AdhesiveDetails
                                        .OrderBy(c => c.ADName)
                                        .Select(x => new { Id = x.ADID, Value = x.ADName });
                }
                else
                {
                    adhesiveDetails = _context.AdhesiveDetails
                                        .Where(x => x.ADName.Contains(adhesive))
                                        .OrderBy(c => c.ADName)
                                        .Select(x => new { Id = x.ADID, Value = x.ADName });
                }
                return Ok(adhesiveDetails);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("fillcloth")]
        public IActionResult FillCloth(string cloth)
        {
            try
            {
                object clothDetails;
                if (string.IsNullOrEmpty(cloth))
                {
                    clothDetails = _context.ClothDetails
                                        .OrderBy(c => c.Chname)
                                        .Select(x => new { Id = x.Chid, Value = x.Chname });
                }
                else
                {
                    clothDetails = _context.ClothDetails
                                        .Where(x => x.Chname.Contains(cloth))
                                        .OrderBy(c => c.Chname)
                                        .Select(x => new { Id = x.Chid, Value = x.Chname });
                }
                return Ok(clothDetails);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("filltanning")]
        public IActionResult FillTanning(string tanning)
        {
            try
            {
                object panningDetails;
                if (string.IsNullOrEmpty(tanning))
                {
                    panningDetails = _context.PanningDetails
                                        .OrderBy(c => c.PanningName)
                                        .Select(x => new { Id = x.PanningId, Value = x.PanningName });
                }
                else
                {
                    panningDetails = _context.PanningDetails
                                        .Where(x => x.PanningName.Contains(tanning))
                                        .OrderBy(c => c.PanningName)
                                        .Select(x => new { Id = x.PanningId, Value = x.PanningName });
                }
                return Ok(panningDetails);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("fillselection")]
        public IActionResult FillSelection(string selection)
        {
            try
            {
                object selectionDetails;
                if (string.IsNullOrEmpty(selection))
                {
                    selectionDetails = _context.SelectionDetails
                                        .OrderBy(c => c.Selection)
                                        .Select(x => new { Id = x.SelectionID, Value = x.Selection });
                }
                else
                {
                    selectionDetails = _context.SelectionDetails
                                        .Where(x => x.Selection.Contains(selection))
                                        .OrderBy(c => c.Selection)
                                        .Select(x => new { Id = x.SelectionID, Value = x.Selection });
                }
                return Ok(selectionDetails);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("fillsupplier")]
        public IActionResult FillSupplier()
        {
            try
            {
                object supplierDetails;
                supplierDetails = _context.SupplierDetails
                                         .OrderBy(c => c.SupplierName)
                                         .Select(x => new { Id = x.SupplierId });
                return Ok(supplierDetails);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSupplierList")]
        public IQueryable<SupplierDetails> GetSupplierList()
        {
            return _context.SupplierDetails.OrderBy(x => x.SupplierName).AsQueryable();
        }

        [Route("GetRMSupplier/{RItemId}")]
        public IQueryable<RItemSupp> GetRMSupplier(long RItemId)
        {
            return _context.RItemSupp.Where(x => x.RItem_Id == RItemId).AsQueryable();
        }

        [Route("GetRMSupplier/{RItemId}/{Suppid}")]
        public IActionResult GetRMSupplier(long RItemId, long Suppid)
        {
            try
            {
                //var selectionDetails = _context.RItemSupp
                //                           .Where(x => x.RItem_Id == RItemId && x.SupplierId == Suppid)
                //                           .Select(x => new { Price = x.Price });
                //return Ok(selectionDetails);

                //double price = 0;
                //var rm = _context.RItemSupp.Where(x => x.RItem_Id == RItemId && x.SupplierId == Suppid).LastOrDefault();
                //price = rm.Price;
                //return Ok(price);

                var rm = _context.RItemSupp.Where(x => x.RItem_Id == RItemId && x.SupplierId == Suppid).LastOrDefault();
                return Ok(rm);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
            //return Ok( _context.RItemSupp.Where(x => x.RItem_Id == RItemId && x.SupplierId ==Suppid).AsQueryable());
        }

        [Route("GetRawMaterial/{id}")]
        public IActionResult GetRawMaterial(int id)
        {
            try
            {
                //return _context.RitemMaster.Where(r => r.RmCode.Value == rmCode).FirstOrDefault();
                var result = _context.Set<ViewRItemShow>().FromSql("SELECT * FROM View_RItemShow where RItem_Id =" + id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("ShowWidth")]
        public IActionResult ShowWidth(string width)
        {
            try
            {
                object WidthDetails;
                if (string.IsNullOrEmpty(width))
                {
                    WidthDetails = _context.WidthDetails
                                        .OrderBy(c => c.WidthName)
                                        .Select(x => new { Id = x.WidthId, Value = x.WidthName });
                }
                else
                {
                    WidthDetails = _context.WidthDetails
                                        .Where(x => x.WidthName.Contains(width))
                                        .OrderBy(c => c.WidthName)
                                        .Select(x => new { Id = x.WidthId, Value = x.WidthName });
                }
                return Ok(WidthDetails);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("ShowGSM")]
        public IActionResult ShowGSM()
        {
            try
            {
                object GSMDetails;
                GSMDetails = _context.GSMDetails
                                    .OrderBy(c => c.GSMName)
                                    .Select(x => new { Id = x.GSMId, Value = x.GSMName });

                return Ok(GSMDetails);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]RitemMaster value)
        {
            try
            {
                _context.RitemMaster.Add(value);
                _context.SaveChanges();
                var result = "Record Save";
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [HttpPut]
        [Route("PutRM")]
        public IActionResult PutRM([FromBody]RitemMaster value)
        {
            try
            {
                var existingRitemMaster = _context.RitemMaster
                                     .Where(x => x.RitemId == value.RitemId)
                                     .Include(x => x.RItemSupp)
                                       .Include(x => x.RMChild)
                                        .Include(x => x.RItemQuickTest)
                                     .SingleOrDefault();

                if (existingRitemMaster != null)
                {
                    existingRitemMaster.MasterBelongTo = value.MasterBelongTo;
                    existingRitemMaster.BelongTo = value.BelongTo;
                    existingRitemMaster.Code = value.Code;
                    //existingRitemMaster.RmCode = value.RmCode;
                    existingRitemMaster.Name = value.Name;
                    existingRitemMaster.FullName = value.Name;
                    //existingRitemMaster.Finish = value.Finish;
                    //existingRitemMaster.Thickness = value.Thickness;
                    //existingRitemMaster.Colour = value.Colour;
                    //existingRitemMaster.Shape = value.Shape;
                    //existingRitemMaster.SizeName = value.SizeName;
                    //existingRitemMaster.Metal = value.Metal;
                    //existingRitemMaster.Quality = value.Quality;
                    //existingRitemMaster.Stucture = value.Stucture;
                    //existingRitemMaster.Selection = value.Selection;
                    //existingRitemMaster.Adhesive = value.Adhesive;
                    //existingRitemMaster.Cloth = value.Cloth;

                    //existingRitemMaster.AverageArea = value.AverageArea;
                    //existingRitemMaster.StrenthTest = value.StrenthTest;
                    //existingRitemMaster.Base = value.Base;
                    //existingRitemMaster.DyedSpe = value.DyedSpe;
                    //existingRitemMaster.EuNorms = value.EuNorms;
                    //existingRitemMaster.Dnsity = value.Dnsity;
                    //existingRitemMaster.ColorFast = value.ColorFast;
                    existingRitemMaster.Punit = value.Punit;
                    existingRitemMaster.CostUnit = value.CostUnit;
                    existingRitemMaster.ConversionFactor = value.ConversionFactor;
                    existingRitemMaster.MinStock = value.MinStock;
                    existingRitemMaster.MaxStock = value.MaxStock;
                    existingRitemMaster.Was = value.Was;
                    existingRitemMaster.BomWasPercent = value.BomWasPercent;
                    //existingRitemMaster.MaxWastage = value.MaxWastage;

                    //existingRitemMaster.Width = value.Width;
                    //existingRitemMaster.GSM = value.GSM;
                    //existingRitemMaster.RMBrandId = 1;//value.RMBrandId;
                    //existingRitemMaster.Cartoon = value.Cartoon;
                    //existingRitemMaster.NickleFree = value.NickleFree;
                    //existingRitemMaster.PanningWay = value.PanningWay;
                    existingRitemMaster.Description = value.Description;
                    existingRitemMaster.SuppCode = value.SuppCode;

                    existingRitemMaster.HSNId = value.HSNId;
                    existingRitemMaster.IsCertificateRequiredForPurchase = value.IsCertificateRequiredForPurchase;

                    existingRitemMaster.RMUpdateOn = value.RMUpdateOn;
                    existingRitemMaster.RMUpdateUser = value.RMUpdateUser;
                    existingRitemMaster.RMReviewStatus = value.RMReviewStatus;
                    existingRitemMaster.RMReviewOn = value.RMReviewOn;
                    existingRitemMaster.RMReviewUser = value.RMReviewUser;
                    existingRitemMaster.Title = value.Title;
                    existingRitemMaster.CLID = value.CLID;
                    existingRitemMaster.SizeID = value.SizeID;

                    existingRitemMaster.ActualPurPrice = value.ActualPurPrice;
                    if (value.RItemSupp != null)
                    {
                        foreach (var childModel in value.RItemSupp)
                        {
                            RItemSupp newChild = new RItemSupp();
                            newChild.RItemSuppID = childModel.RItemSuppID;
                            newChild.RItem_Id = childModel.RItem_Id;
                            newChild.SupplierId = childModel.SupplierId;
                            newChild.SuppUnit = childModel.SuppUnit;
                            newChild.SupplierRMCode = childModel.SupplierRMCode;

                            var existingChild = existingRitemMaster.RItemSupp
                                .Where(c => c.RItem_Id == childModel.RItem_Id && c.SupplierId == childModel.SupplierId)
                                .SingleOrDefault();

                            if (existingChild == null)
                            {
                                existingRitemMaster.RItemSupp.Add(newChild);
                            }
                        }
                    }

                    foreach (var ExistingRMChild in existingRitemMaster.RMChild.ToList())
                    {
                        _context.RMChild.Remove(ExistingRMChild);
                    }
                    if (value.RMChild != null)
                    {
                        foreach (var rmchild in value.RMChild)
                        {
                            RMChild newChild = new RMChild();
                            newChild.RMChildId = rmchild.RMChildId;
                            newChild.RItem_Id = rmchild.RItem_Id;
                            newChild.RMPropertiesID = rmchild.RMPropertiesID;
                            newChild.RMPropertiesValueID = rmchild.RMPropertiesValueID;
                            newChild.RMPValue = rmchild.RMPValue;
                            existingRitemMaster.RMChild.Add(newChild);

                        }
                    }

                    foreach (var ExistingRItemQuickTest in existingRitemMaster.RItemQuickTest.ToList())
                    {
                        _context.RItemQuickTest.Remove(ExistingRItemQuickTest);
                    }
                    if (value.RItemQuickTest != null)
                    {
                        foreach (var QuickTest in value.RItemQuickTest)
                        {
                            RItemQuickTest newQuicktest = new RItemQuickTest();
                            newQuicktest.QuickTestId = QuickTest.QuickTestId;
                            newQuicktest.RItem_Id = QuickTest.RItem_Id;
                            newQuicktest.RItemQuickTestId = QuickTest.RItemQuickTestId;

                            existingRitemMaster.RItemQuickTest.Add(newQuicktest);
                        }
                    }
                    _context.RitemMaster.Update(existingRitemMaster);
                    _context.SaveChanges();
                }
                else
                {
                    _context.RitemMaster.Add(value);
                    _context.SaveChanges();
                }
                var result = "Record Save";
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]RitemMaster value)
        {
            try
            {
                var existingRM = _context.RitemMaster.Find(value.RitemId);
                if (existingRM == null)
                {
                    return Ok("Not Found");
                }
                existingRM.BelongTo = value.BelongTo;
                existingRM.MasterBelongTo = value.MasterBelongTo;
                existingRM.RMUpdateOn = value.RMUpdateOn;
                existingRM.RMUpdateUser = value.RMUpdateUser;


                _context.RitemMaster.Update(existingRM);
                _context.SaveChanges();
                return Ok("Record Move");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingRM = _context.RitemMaster
                                                     .Where(x => x.RitemId == id)
                                                     .Include(x => x.RItemSupp)
                                                     .Include(x => x.RItemQuickTest)
                                                     .Include(x => x.RMChild)
                                                     .SingleOrDefault();
                if (existingRM != null)
                {
                    var rmpricehistory = _context.RMSupplierPriceHistory.Where(x=> x.RItem_Id ==id).ToList();
                    _context.RMSupplierPriceHistory.RemoveRange(rmpricehistory);

                    // Delete children
                    foreach (var existingRitemSupp in existingRM.RItemSupp.ToList())
                    {
                        _context.RItemSupp.Remove(existingRitemSupp);
                    }

                    foreach (var existingRItemQuickTest in existingRM.RItemQuickTest.ToList())
                    {
                        _context.RItemQuickTest.Remove(existingRItemQuickTest);
                    }

                    foreach (var _rmchild in existingRM.RMChild.ToList())
                    {
                        _context.RMChild.Remove(_rmchild);
                    }
                    _context.RitemMaster.Remove(existingRM);
                    _context.SaveChanges();
                }
                else
                {
                    return Ok("Not Found");
                }
                return Ok("Record Deleted");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("adhesive")]
        public IActionResult Adhesive(string adhesive)
        {
            try
            {
                object adhesiveDetails;
                if (string.IsNullOrEmpty(adhesive))
                {
                    adhesiveDetails = _context.AdhesiveDetails
                                        .OrderBy(c => c.ADName)
                                        .Select(x => new { Id = x.ADID, Value = x.ADName });
                }
                else
                {
                    adhesiveDetails = _context.AdhesiveDetails
                                        .Where(x => x.ADName.Contains(adhesive))
                                        .OrderBy(c => c.ADName)
                                        .Select(x => new { Id = x.ADID, Value = x.ADName });
                }
                return Ok(adhesiveDetails);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("ViewRItemShow")]
        public IQueryable<ViewRItemShow> ViewRItemShow()
        {
            return _context.ViewRItemShow.Where(x => x.Code != "-").OrderByDescending(x => x.RitemId).AsQueryable();
        }

        [Route("GetViewRItemShow")]
        public IActionResult GetViewRItemShow(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    var rm = _context.ViewRItemShow.OrderByDescending(x => x.RitemId).Take(20); //.Where(x => x.Code != "-")
                    return Ok(rm);
                }
                else
                {
                    var rm = _context.ViewRItemShow
                                    .Where(x => (x.CategoryName.Contains(search) || x.SubCategoryName.Contains(search) || x.Code.Contains(search) || x.Name.Contains(search)))  //(x.Code != "-") &&
                                    .OrderByDescending(x => x.RitemId);
                    return Ok(rm);
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PutRMRate")]
        public IActionResult PutRMRate([FromBody]RitemMaster value)
        {
            try
            {
                foreach (var childModel in value.RItemSupp)
                {
                    var existingChild = _context.RItemSupp
                        .Where(c => c.RItemSuppID == childModel.RItemSuppID)
                        .SingleOrDefault();

                    if (existingChild != null)
                    {
                        existingChild.Price = childModel.Price;
                        existingChild.MinDays = childModel.MinDays;
                        existingChild.SuppUnit = childModel.SuppUnit;
                        existingChild.SupplierRMCode = childModel.SupplierRMCode;
                        _context.RItemSupp.Update(existingChild);
                    }
                }
                _context.SaveChanges();

                var result = "Record Save";
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetRitemByCatid/{catid}")]
        public IActionResult GetRitemByCatid(long catid)
        {
            try
            {
                //if (catid != 0)
                //{
                var RM = _context.ViewRItemShow.Where(x => x.MasterBelongTo == catid).OrderBy(x => x.Name);
                return Ok(RM);
                //} 
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [Route("PutRMCostPrice")]
        public IActionResult PutRMCostPrice([FromBody]List<RitemMaster> value)
        {
            try
            {
                for (int i = 0; i < value.Count; i++)
                {
                    if (value[i].RitemId != 0)
                    {
                        var _ritemMaster = _context.RitemMaster.Find(value[i].RitemId);
                        _ritemMaster.CostPrice = value[i].CostPrice;
                        _context.RitemMaster.Update(_ritemMaster);
                    }
                }
                _context.SaveChanges();
                var result = "Record Save";
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("ViewRMSupplier/{CategoryID}")]
        public IActionResult ViewRMSupplier(long CategoryID)
        {
            try
            {
                if (CategoryID == 0)
                    return Ok(_context.ViewRMSupplier.OrderBy(x => x.Full_Name));
                else
                {
                    var RM = _context.ViewRMSupplier
                                    .Where(x => x.CategoryID == CategoryID)
                                    .OrderBy(x => x.Full_Name);
                    return Ok(RM);
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("ViewRMSupplier/{rmcatid}/{rmscatid}/{ritemid}/{supplierid}")]
        public IActionResult ViewRMSupplier(long rmcatid, long rmscatid, long ritemid, long supplierid)
        {
            try
            {
                if (supplierid == 0)
                {
                    if (ritemid == 0 && rmscatid == 0)
                    {
                        return Ok(_context.ViewRMSupplier.OrderBy(x => x.CategoryName).ThenBy(x => x.SubCategoryName).ThenBy(x => x.Full_Name)
                            .Where(x => x.CategoryID == rmcatid));

                    }
                    else if (ritemid == 0 && rmscatid != 0)
                    {
                        return Ok(_context.ViewRMSupplier.OrderBy(x => x.CategoryName).ThenBy(x => x.SubCategoryName).ThenBy(x => x.Full_Name)
                            .Where(x => x.SubCategoryID == rmscatid));
                    }
                    else
                    {
                        return Ok(_context.ViewRMSupplier.OrderBy(x => x.CategoryName).ThenBy(x => x.SubCategoryName).ThenBy(x => x.Full_Name)
                            .Where(x => x.RItem_Id == ritemid));
                    }
                }
                else
                {
                    if (ritemid == 0 && rmscatid == 0 && rmcatid == 0)
                    {
                        return Ok(_context.ViewRMSupplier.OrderBy(x => x.CategoryName).ThenBy(x => x.SubCategoryName).ThenBy(x => x.Full_Name)
                                                .Where(x => x.SupplierId == supplierid));
                    }
                    else if (ritemid == 0 && rmscatid == 0)
                    {
                        return Ok(_context.ViewRMSupplier.OrderBy(x => x.CategoryName).ThenBy(x => x.SubCategoryName).ThenBy(x => x.Full_Name)
                            .Where(x => x.CategoryID == rmcatid && x.SupplierId == supplierid));

                    }
                    else if (ritemid == 0 && rmscatid != 0)
                    {
                        return Ok(_context.ViewRMSupplier.OrderBy(x => x.CategoryName).ThenBy(x => x.SubCategoryName).ThenBy(x => x.Full_Name)
                            .Where(x => x.SubCategoryID == rmscatid && x.SupplierId == supplierid));
                    }
                    else
                    {
                        return Ok(_context.ViewRMSupplier.OrderBy(x => x.CategoryName).ThenBy(x => x.SubCategoryName).ThenBy(x => x.Full_Name)
                            .Where(x => x.RItem_Id == ritemid && x.SupplierId == supplierid));
                    }

                }


                //if (CategoryID == 0)
                //    return Ok(_context.ViewRMSupplier.OrderBy(x => x.Full_Name));
                //else
                //{
                //    var RM = _context.ViewRMSupplier
                //                    .Where(x => x.CategoryID == CategoryID)
                //                    .OrderBy(x => x.Full_Name);
                //    return Ok(RM);
                //}
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSupplierRecord/{rmid}")]
        public IActionResult GetSupplierRecord(long rmid)
        {
            try
            {
                List<ViewRMSupplier> supplierrec = new List<Domain.Entities.ViewRMSupplier>();
                supplierrec = _context.ViewRMSupplier.Where(x => x.RItem_Id == rmid)
                                         .OrderBy(x => x.SupplierId).ToList();
                return Ok(supplierrec);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetSupplierListFromRM/{rmid}")]
        public IActionResult GetSupplierListFromRM(long rmid)
        {
            try
            {
                object supplierrec;
                supplierrec = _context.ViewRMSupplier.Where(x => x.RItem_Id == rmid)
                                         .OrderBy(x => x.SupplierId)
                                         .Select(x => new { Id = x.SupplierId, Value = x.SupplierName });
                return Ok(supplierrec);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetRitemRecord/{rmcatid}/{rmscatid}/{ritemid}")]
        public IActionResult GetRitemRecord(long rmcatid, long rmscatid, long ritemid)
        {
            try
            {
                if (ritemid == 0 && rmscatid == 0)
                {
                    return Ok(_context.ViewRItemShow.OrderBy(x => x.CategoryName).ThenBy(x => x.SubCategoryName).ThenBy(x => x.Full_Name)
                        .Where(x => x.CategoryID == rmcatid));
                }
                else if (ritemid == 0 && rmscatid != 0)
                {
                    return Ok(_context.ViewRItemShow.OrderBy(x => x.CategoryName).ThenBy(x => x.SubCategoryName).ThenBy(x => x.Full_Name)
                        .Where(x => x.SubCategoryID == rmscatid));
                }
                else
                {
                    return Ok(_context.ViewRItemShow.OrderBy(x => x.CategoryName).ThenBy(x => x.SubCategoryName).ThenBy(x => x.Full_Name)
                        .Where(x => x.RitemId == ritemid));
                }

                //var RM = _context.ViewRItemShow.Where(x => x.MasterBelongTo == catid);
                //return Ok(RM);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetList/{id}")]
        public IActionResult GetList(long id)
        {
            try
            {
                object rmscatlist = _context.RitemMaster.Where(x => x.BelongTo == id)
                                         .OrderBy(x => x.Code)
                                         .Select(x => new { Id = x.RitemId, Value = x.Code });
                return Ok(rmscatlist);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("RMForProduct/{ritemid}")]
        public IActionResult RMForProduct(long ritemid)  //long rmcatid, long rmscatid, 
        {
            try
            {
                var CostingIdlist = _context.ViewCostingRM.Where(x => x.RItemID == ritemid)
                                         .OrderBy(x => x.CostingID)
                                         .Select(x => x.CostingID).ToList();
                var rmforproduct = _context.ViewShowCosting.Where(x => CostingIdlist.Contains(x.CostingID)).OrderBy(x => x.Code);
                return Ok(rmforproduct);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("RMMinimumLevel")]
        public IActionResult RMMinimumLevel()
        {
            try
            {
                var CostingIdlist = _context.ViewBelowMinimumLevel
                                         .OrderBy(x => x.RItem_Id)
                                         .Select(x => x.RItem_Id).ToList();
                var rmminimumlevel = _context.ViewBelowMinimumLevel.Where(x => CostingIdlist.Contains(x.RItem_Id));
                return Ok(rmminimumlevel);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetUnusedRmList")]
        public IActionResult GetUnusedRmList()
        {
            try
            {
                var CostingIdlist = _context.ViewUnusedRMList
                                         .OrderBy(x => x.RItem_Id)
                                         .Select(x => x.RItem_Id).ToList();
                var unusedrm = _context.ViewUnusedRMList.Where(x => CostingIdlist.Contains(x.RItem_Id));
                return Ok(unusedrm);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetRMList")]
        public IActionResult GetRMList()
        {
            try
            {
                object rmlist;
                rmlist = _context.RitemMaster
                                    .OrderBy(c => c.Name)
                                    .Select(x => new { Id = x.RitemId, Value = x.Code + " @ " + x.FullName }).ToList();
                return Ok(rmlist);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetRMSCatList")]
        public IActionResult GetRMSCatList()
        {
            try
            {
                object rmscat;
                rmscat = _context.RMSubCategory.Where(x => x.CategoryID == 12)
                                    .OrderBy(c => c.SubCategoryName)
                                    .Select(x => new { Id = x.SubCategoryID, Value = x.SubCategoryName }).ToList();
                return Ok(rmscat);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetRMList/{rmscatid}")]
        public IActionResult GetRMList(long rmscatid)
        {
            try
            {
                object rm;
                rm = _context.RitemMaster.Where(x => x.BelongTo == rmscatid)
                                    .OrderBy(c => c.FullName)
                                    .Select(x => new { Id = x.RitemId, Value = x.FullName }).ToList();
                return Ok(rm);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetCostingRMList/{ritemid}")]
        public IActionResult GetCostingRMList(long ritemid)
        {
            try
            {
                var rmcatid = _context.RitemMaster.Where(x => x.RitemId == ritemid).Select(x => x.MasterBelongTo).SingleOrDefault();

                var rm = _context.ViewRItemShow.Where(x => x.MasterBelongTo == rmcatid)
                                    .OrderBy(c => c.Full_Name)
                                    .Select(x => new { Id = x.RitemId, Value = x.Full_Name }).ToList();
                return Ok(rm);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("RMList")]
        public IActionResult RMList([FromBody]RMSearch RMS)
        {
            try
            {
                IQueryable<ViewRItemShow> query = _context.ViewRItemShow;
                if (RMS.RMCategory_ID > 0)
                    query = query.Where(x => x.CategoryID == RMS.RMCategory_ID);
                if (RMS.RMSubCategory_ID > 0)
                    query = query.Where(x => x.SubCategoryID == RMS.RMSubCategory_ID);
                if (!string.IsNullOrEmpty(RMS.RMCode))
                    query = query.Where(x => x.Code.Contains(RMS.RMCode));
                if (!string.IsNullOrEmpty(RMS.RMName))
                    query = query.Where(x => x.Name.Contains(RMS.RMName));
                if (RMS.RMBrandId > 0)
                    query = query.Where(x => x.RMBrandId == RMS.RMBrandId);

                if (!string.IsNullOrEmpty(RMS.Finish))
                    query = query.Where(x => x.Finish.Contains(RMS.Finish));
                if (!string.IsNullOrEmpty(RMS.Thickness))
                    query = query.Where(x => x.Thickness.Contains(RMS.Thickness));
                if (!string.IsNullOrEmpty(RMS.SizeName))
                    query = query.Where(x => x.SizeName.Contains(RMS.SizeName));
                //if (RMS.Color_ID > 0)
                //    query = query.Where(x => x.CLId == RMS.Color_ID);
                //if (!string.IsNullOrEmpty(RMS.Color) || RMS.Color != "-- Select --")
                if (RMS.Color != "-- Select --")
                    query = query.Where(x => x.Colour == RMS.Color);
                if (!string.IsNullOrEmpty(RMS.RMDescription))
                    query = query.Where(x => x.Description.Contains(RMS.RMDescription));

                if (RMS.ChkCreateDateFromInt == 1 && RMS.ChkCreateDateToInt == 1)
                    query = query.Where(x => x.CreatedDate >= (RMS.CreateDateFrom) && x.CreatedDate <= RMS.CreateDateTo);
                if (RMS.ChkCreateDateFromInt == 1 && RMS.ChkCreateDateToInt == 0)
                    query = query.Where(x => x.CreatedDate == (RMS.CreateDateFrom));
                if (RMS.ChkRMUpdateOnInt == 1)
                    query = query.Where(x => x.RMUpdateOn == RMS.RMUpdateOn);
                if (RMS.ChkRMReviewOnInt == 1)
                    query = query.Where(x => x.RMReviewOn == RMS.RMReviewOn);

                query = query.OrderBy(x => x.Code);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetRMFileRecord/{id}")]
        public IActionResult GetRMFileRecord(long id)
        {
            try
            {
                var productfile = _context.ViewRMFile
                                        .Where(x => x.RItem_Id == id)
                                        .OrderBy(x => x.RMFileId);
                return Ok(productfile);

            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        [Route("GetNewId")]
        public IActionResult GetNewId()
        {
            try
            {
                long RMFileId = 0;
                var lastRMFile = _context.RMFile.OrderBy(x => x.RMFileId).LastOrDefault();
                RMFileId = (lastRMFile == null ? 0 : lastRMFile.RMFileId) + 1;
                return Ok(RMFileId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PostRMFile")]
        public IActionResult PostRMFile([FromBody]RMFile value)
        {
            try
            {
                _context.RMFile.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("DeleteRMFile/{id}")]
        public IActionResult DeleteRMFile(long id)
        {
            var existing = _context.RMFile.Find(id);
            if (existing != null)
            {
                _context.RMFile.Remove(existing);
                _context.SaveChanges();
                return Ok("1");
            }
            else
            {
                return Ok("0");
            }
        }

        [Route("RMFileName/{id}")]
        public IActionResult RMFileName(long id)
        {
            try
            {
                var filename = _context.RMFile.Where(x => x.RMFileId == id).Select(x => x.FileLocation).FirstOrDefault(); ;
                return Ok(filename);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPut]
        [Route("PutRMReviewStatus")]
        public IActionResult PutRMReviewStatus([FromBody]RitemMaster value)
        {
            try
            {
                var existing = _context.RitemMaster.Find(value.RitemId);
                existing.RMReviewStatus = value.RMReviewStatus;
                existing.RMReviewOn = value.RMReviewOn;
                existing.RMReviewUser = value.RMReviewUser;

                _context.RitemMaster.Update(existing);
                _context.SaveChanges();
                return Ok("Record Updated");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetRMListFromSupplier/{supplierid}")]
        public IActionResult GetRMListFromSupplier(long supplierid)
        {
            try
            {
                object supplierrec;
                supplierrec = _context.ViewRMSupplier.Where(x => x.SupplierId == supplierid)
                                         .OrderBy(x => x.RItem_Id)
                                         .Select(x => new { Id = x.RItem_Id, Value = x.Code + " @ "+ x.Full_Name });
                return Ok(supplierrec);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetNewRMChildId")]
        public IActionResult GetNewRMChildId()
        {
            try
            {
                long rmchildid = 0;
                var lastchild = _context.RMChild.OrderBy(x => x.RMChildId).LastOrDefault();
                rmchildid = (lastchild == null ? 0 : lastchild.RMChildId) + 1;
                return Ok(rmchildid);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        //+++++++++++
        [Route("GetNewRMPriceHistoryId")]
        public IActionResult GetNewRMPriceHistoryId()
        {
            try
            {
                long Id = 0;
                var last = _context.RMSupplierPriceHistory.OrderBy(x => x.RMPriceHistoryId).LastOrDefault();
                Id = (last == null ? 0 : last.RMPriceHistoryId) + 1;
                return Ok(Id);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PostRMSupplierPriceHistory")]
        public IActionResult PostRMSupplierPriceHistory([FromBody]List<RMSupplierPriceHistory> value)
        {
            try
            {
                for (int i = 0; i < value.Count; i++)
                {
                    _context.RMSupplierPriceHistory.Add(value[i]);
                }
                _context.SaveChanges();
                var result = "Record Save";
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [Route("GetRMSupplierPriceHistory/{ritmid}/{supplierid}")]
        public IActionResult GetRMSupplierPriceHistory(long ritmid, long supplierid)
        {
            try
            {

                return Ok(_context.ViewRMSupplierPriceHistory
                    .Where(x => x.RItem_Id == ritmid && x.SupplierId == supplierid)
                    .OrderByDescending(x => x.RMPriceHistoryId));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        [Route("GetRMSupplierPriceHistoryList")]
        public IActionResult GetRMSupplierPriceHistoryList([FromBody]RMSearch RMS)
        {
            try
            {
                IQueryable<ViewRMSupplierPriceHistory> query = _context.ViewRMSupplierPriceHistory;
                if (RMS.SupplierId > 0)
                    query = query.Where(x => x.SupplierId == RMS.SupplierId);
                if (RMS.RItemId > 0)
                    query = query.Where(x => x.RItem_Id == RMS.RItemId);
                if (!string.IsNullOrEmpty(RMS.RMCode))
                    query = query.Where(x => x.Code.Contains(RMS.RMCode));
                if (!string.IsNullOrEmpty(RMS.RMName))
                    query = query.Where(x => x.Name.Contains(RMS.RMName));
                if (RMS.ChkCreateDateFromInt == 1 && RMS.ChkCreateDateToInt == 1)
                    query = query.Where(x => x.EntryDate >= (RMS.CreateDateFrom) && x.EntryDate <= RMS.CreateDateTo);
                if (RMS.ChkCreateDateFromInt == 1 && RMS.ChkCreateDateToInt == 0)
                    query = query.Where(x => x.EntryDate == (RMS.CreateDateFrom));

                query = query.OrderByDescending(x => x.RMPriceHistoryId);
                var record = query.ToList();
                return Ok(record);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("PutRMDetails")]
        public IActionResult PutRMDetails([FromBody]List<RitemMaster> value)
        {
            try
            {
                foreach (RitemMaster onerecord in value)
                {
                    var _ritemMaster = _context.RitemMaster.Find(onerecord.RitemId);
                    if (_ritemMaster != null)
                    {
                        _ritemMaster.Punit = onerecord.Punit;
                        _ritemMaster.CostUnit = onerecord.CostUnit;
                        _ritemMaster.ConversionFactor = onerecord.ConversionFactor;
                        _ritemMaster.ActualPurPrice = onerecord.ActualPurPrice;
                        _context.RitemMaster.Update(_ritemMaster);
                    }
                }              
                _context.SaveChanges();
                var result = "Record Save";
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
 

        [Route("RMSizeForProduct/{ritemid}")]
        public IActionResult RMSizeForProduct(long ritemid)
        {
            try
            {
                var rmsizecosting = _context.ViewShowRMSizeCosting.Where(x => x.RItemID == ritemid)
                                         .OrderBy(x => x.CostingID);
                return Ok(rmsizecosting);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
